﻿using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Requests.Users;
using FMI.WeAzure.Boxing.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dto = FMI.WeAzure.Boxing.Contracts.Dto;

namespace FMI.WeAzure.Boxing.Business.Handlers.Users
{
    public class GetAllUsersHandler : BaseHandler, IRequestHandler<GetAllUsersRequest, IEnumerable<Dto.User>>
    {
        public GetAllUsersHandler(BoxingDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Dto.User>> HandleAsync(GetAllUsersRequest request)
        {
            int successId = (int)PredictionResultEnum.Correct;
            Expression<Func<User, object>> orderer;
            switch (request.OrderByColumn)
            {
                case Dto.UserOrderColumn.Rating: 
                    orderer = (user) => 
                        Context.Predictions.Where(p =>
                            p.MadeBy.Username == user.Username && 
                            p.MadeFor.Active == true &&
                            p.MadeFor.Winner != null && 
                            p.PredictionResult.Id == successId).Count() 
                        /
                        Context.Predictions.Where(p =>
                            p.MadeBy.Username == user.Username &&
                            p.MadeFor.Active == true &&
                            p.MadeFor.Winner != null).Count();
                    break;
                case Dto.UserOrderColumn.FullName:
                    orderer = (user) => user.FullName;
                    break;
                default:
                    throw new Exception("Get all users request did not pass through validation successfully");
            }
            var orderedResult = request.SortOrder == Contracts.Dto.SortOrder.Ascending ?
                Context.Users.OrderBy(orderer)
                : Context.Users.OrderByDescending(orderer);

            // TODO: Fix return results
            var result = (await 
                orderedResult
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync())
                .Select(user => new Dto.User()
                {
                    UserName = user.Username,
                    FullName = user.FullName,
                    Rating = user.CalculateRating()
                });



            return result;
        }
    }
}
