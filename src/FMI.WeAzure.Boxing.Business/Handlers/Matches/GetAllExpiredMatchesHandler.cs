using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Requests.Matches;
using FMI.WeAzure.Boxing.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Business.Handlers.Matches
{
    public class GetAllExpiredMatchesHandler : BaseHandler, IRequestHandler<GetAllExpiredMatchesRequest, IEnumerable<Match>>
    {
        public GetAllExpiredMatchesHandler(BoxingDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Match>> HandleAsync(GetAllExpiredMatchesRequest request)
        {
            var expiredMatches =
                await Context
                .BoxingMatches
                .Where(m => m.IsExpired)
                .ToListAsync();

            return expiredMatches
                    .Select(m => new Match()
                    {
                        Address = m.Address,
                        Date = m.Time,
                        FirstBoxer = m.FirstBoxer.Id,
                        SecondBoxer = m.SecondBoxer.Id,
                        Description = m.Description,
                        Predictions = null // TODO
                    });
        }
    }
}
