using FMI.WeAzure.Boxing.Business.Interfaces;
using Dto = FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Requests.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FMI.WeAzure.Boxing.Database;

namespace FMI.WeAzure.Boxing.Business.Handlers.Matches
{
    public class GetAllMatchesHandler : BaseHandler, IRequestHandler<GetAllMatchesRequest, IEnumerable<Dto.Match>>
    {
        public GetAllMatchesHandler(BoxingDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Dto.Match>> HandleAsync(GetAllMatchesRequest request)
        {
            var matchSet = Context.BoxingMatches.AsQueryable();
            // Apply filtering if needed
            if (!string.IsNullOrEmpty(request.Search))
            {
                matchSet =
                    matchSet
                    .Where(m => m.Address.Contains(request.Search) || m.FirstBoxer.Name.Contains(request.Search) || m.SecondBoxer.Name.Contains(request.Search));
            }

            matchSet = request.SortOrder == Contracts.Dto.SortOrder.Ascending ?
                matchSet.OrderBy(m => m.Time)
                : matchSet.OrderByDescending(m => m.Time);


            var result = (await
                matchSet
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync())
                .Select(m => new Dto.Match()
                {
                    Id = m.Id,
                    Address = m.Address,
                    Date = m.Time,
                    FirstBoxer = m.FirstBoxer.Id,
                    SecondBoxer = m.SecondBoxer.Id,
                    Description = m.Description,
                    Predictions = m.Predictions.Select(p =>
                        new Dto.Prediction()
                        {
                            Id = p.Id,
                            User = p.MadeBy.Username,
                            Predicted = (Dto.Prediction.PredictionKind) p.PredictionResult.Id
                        })
                });

            return result;
        }
    }
}
