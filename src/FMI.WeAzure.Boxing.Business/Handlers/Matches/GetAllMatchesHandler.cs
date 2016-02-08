using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Dto;
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
    public class GetAllMatchesHandler : BaseHandler, IRequestHandler<GetAllMatchesRequest, IEnumerable<Match>>
    {
        public GetAllMatchesHandler(BoxingDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Match>> HandleAsync(GetAllMatchesRequest request)
        {
            var orderedSet = request.SortOrder == Contracts.Dto.SortOrder.Ascending ?
                Context.BoxingMatches.OrderBy(m => m.Time)
                : Context.BoxingMatches.OrderByDescending(m => m.Time);

            var result = (await
                orderedSet
                .Skip(request.Skip)
                .Take(request.Take)
                .ToListAsync())
                .Select(m => new Match()
                {
                    Address = m.Address,
                    Date = m.Time,
                    FirstBoxer = m.FirstBoxer.Id,
                    SecondBoxer = m.SecondBoxer.Id,
                    Description = m.Description,
                    Predictions = null // TODO
                });

            return result;
        }
    }
}
