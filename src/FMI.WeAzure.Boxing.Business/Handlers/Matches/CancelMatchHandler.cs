using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Requests.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Business.Exceptions;
using System.Diagnostics;
using System.Data.Entity;
using FMI.WeAzure.Boxing.Database;

namespace FMI.WeAzure.Boxing.Business.Handlers.Matches
{
    public class CancelMatchHandler : BaseHandler, ICommandHandler<CancelMatchRequest>
    {
        public CancelMatchHandler(BoxingDbContext context) : base(context)
        {

        }

        public async Task<Unit> HandleAsync(CancelMatchRequest request)
        {
            var match = await Context.BoxingMatches.FindAsync(request.MatchId);
            if (match == null)
            {
                throw new EntityDoesNotExistException("No such match");
            }
            Debug.Assert(match.Winner == null);

            match.Active = false;
            var matchCancelledResult = (await Context.PredictionResults.ToListAsync()).Single(p => p.Id == (int)PredictionResultEnum.MatchCanceled);
            foreach (var prediction in match.Predictions)
            {
                prediction.PredictionResult = matchCancelledResult;
            }
            await Context.SaveChangesAsync();

            return Unit.Instance;
        }
    }
}
