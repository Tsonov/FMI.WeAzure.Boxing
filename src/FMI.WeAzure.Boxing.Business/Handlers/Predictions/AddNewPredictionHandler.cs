using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Requests.Predictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Business.Exceptions;
using FMI.WeAzure.Boxing.Database;
using System.Data.Entity;

namespace FMI.WeAzure.Boxing.Business.Handlers.Predictions
{
    public class AddNewPredictionHandler : BaseHandler, ICommandHandler<AddNewPredictionRequest>
    {
        public async Task<Unit> HandleAsync(AddNewPredictionRequest request)
        {
            // TODO: Single prediction by user?
            var match = await Context.BoxingMatches.FindAsync(request.MatchId);
            if (match == null)
            {
                throw new EntityDoesNotExistException("Invalid match id");
            }
            var winningBoxer = await Context.Boxers.FindAsync(request.PredictedWinner);
            if (winningBoxer == null)
            {
                throw new EntityDoesNotExistException("Invalid boxer id for winner");
            }
            var user = await Context.Users.FindAsync(request.MadeByUser);
            if (user == null)
            {
                throw new EntityDoesNotExistException("Invalid user");
            }
            var matchNotEndedEntity = (await Context.PredictionResults.ToListAsync()).Single(p => p.Id == (int)PredictionResultEnum.MatchNotEnded);
            // TODO: Additional validations

            var newPrediction = new Prediction()
            {
                MadeBy = user,
                MadeFor = match,
                PredictedWinner = winningBoxer,
                PredictionResult = matchNotEndedEntity
            };
            Context.Predictions.Add(newPrediction);
            await Context.SaveChangesAsync();
            return Unit.Instance;
        }
    }
}
