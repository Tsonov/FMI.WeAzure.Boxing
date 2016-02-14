using FMI.WeAzure.Boxing.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Database;
using Dto = FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Requests.Users;
using FMI.WeAzure.Boxing.Business.Services;
using FMI.WeAzure.Boxing.Common;

namespace FMI.WeAzure.Boxing.Business.Handlers.Users
{
    public class CreateUserHandler : BaseHandler, IRequestHandler<CreateUserRequest, Dto.User>
    {
        private readonly IPasswordService passwordService;

        public CreateUserHandler(IPasswordService passwordService, BoxingDbContext context) : base(context)
        {
            Check.ThrowIfNull(passwordService, "passwordService", "Provided password service can not be null");
            this.passwordService = passwordService;
        }

        public async Task<Dto.User> HandleAsync(CreateUserRequest request)
        {
            var password = passwordService.CreateHash(request.Password);
            var newEntity = new User()
            {
                Username = request.UserName,
                FullName = request.FullName,
                Password = password
            };
            Context.Users.Add(newEntity);
            await Context.SaveChangesAsync();
            return new Dto.User()
            {
                UserName = newEntity.Username,
                FullName = newEntity.FullName,
                Rating = newEntity.CalculateRating()
            };
        }
    }
}
