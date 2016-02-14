using FMI.WeAzure.Boxing.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Contracts.Requests.Matches;
using FMI.WeAzure.Boxing.Business.Exceptions;
using FMI.WeAzure.Boxing.Database;
using System.Data.Entity;

namespace FMI.WeAzure.Boxing.Business.Handlers.Matches
{
    public class SetMatchResultHandler : BaseHandler, ICommandHandler<SetMatchResultRequest>
    {
        public SetMatchResultHandler(BoxingDbContext context) : base(context)
        {

        }

        public async Task<Unit> HandleAsync(SetMatchResultRequest request)
        {
            var match = await Context.BoxingMatches.FindAsync(request.MatchId);
            if (match == null)
            {
                throw new EntityDoesNotExistException("No such match");
            }
            if (match.FirstBoxer.Id != request.Winner && match.SecondBoxer.Id != request.Winner)
            {
                // TODO: exception types
                throw new Exception("Invalid winner id, did not participate");
            }
            match.Winner = match.FirstBoxer.Id == request.Winner ? match.FirstBoxer : match.SecondBoxer;

            // Update all predictions to match
            var correctEntity = (await Context.PredictionResults.ToListAsync()).Single(p => p.Id == (int)PredictionResultEnum.Correct);
            var incorrect = (await Context.PredictionResults.ToListAsync()).Single(p => p.Id == (int)PredictionResultEnum.Incorrect);
            var predictions = match.Predictions.ToList();
            foreach (var prediction in predictions)
            {
                if (prediction.PredictedWinner.Id == request.Winner)
                {
                    prediction.PredictionResult = correctEntity;
                }
                else
                {
                    prediction.PredictionResult = incorrect;
                }
            }
            await Context.SaveChangesAsync();

            return Unit.Instance;
        }
    }
}
