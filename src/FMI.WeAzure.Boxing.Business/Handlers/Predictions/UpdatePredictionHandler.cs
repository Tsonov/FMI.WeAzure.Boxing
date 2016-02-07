using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Requests.Predictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Business.Exceptions;
using System.Data.Entity;
using FMI.WeAzure.Boxing.Database;

namespace FMI.WeAzure.Boxing.Business.Handlers.Predictions
{
    public class UpdatePredictionHandler : BaseHandler, ICommandHandler<UpdatePredictionRequest>
    {
        public async Task<Unit> HandleAsync(UpdatePredictionRequest request)
        {
            // TODO: Ensure proper user sends this
            var prediction = await Context.Predictions.FindAsync(request.Prediction.Id);
            if (prediction == null)
            {
                throw new EntityDoesNotExistException("No such prediction");
            }

            switch (request.Prediction.UserPrediction)
            {
                case Contracts.Dto.Prediction.PredictionKind.FirstBoxerWins:
                    prediction.PredictedWinner = prediction.MadeFor.FirstBoxer;
                    break;
                case Contracts.Dto.Prediction.PredictionKind.SecondBoxerWins:
                    prediction.PredictedWinner = prediction.MadeFor.SecondBoxer;
                    break;
                case Contracts.Dto.Prediction.PredictionKind.Cancel:
                    prediction.PredictionResult = (await Context.PredictionResults.ToListAsync()).Single(p => p.Id == (int)PredictionResultEnum.UserCanceled);
                    break;
                default:
                    throw new Exception("Invalid prediction kind value; validation did not work correctly");
            }

            await Context.SaveChangesAsync();
            return Unit.Instance;
        }
    }
}
