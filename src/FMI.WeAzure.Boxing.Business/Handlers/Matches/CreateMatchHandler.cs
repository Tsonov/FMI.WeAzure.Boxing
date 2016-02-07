using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Requests.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Database;

namespace FMI.WeAzure.Boxing.Business.Handlers.Matches
{
    public class CreateMatchHandler : BaseHandler, ICommandHandler<CreateMatchRequest>
    {
        public async Task<Unit> HandleAsync(CreateMatchRequest request)
        {
            var entity = new BoxingMatch()
            {
                Address = request.MatchInfo.Address,
                FirstBoxer = await Context.Boxers.FindAsync(request.MatchInfo.FirstBoxer),
                SecondBoxer = await Context.Boxers.FindAsync(request.MatchInfo.SecondBoxer),
                Description = request.MatchInfo.Description,
                Time = request.MatchInfo.Date
            };
            Context.BoxingMatches.Add(entity);
            await Context.SaveChangesAsync();

            return Unit.Instance;
        }
    }
}
