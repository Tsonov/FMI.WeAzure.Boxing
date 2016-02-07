using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Requests.Predictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Business.Exceptions;

namespace FMI.WeAzure.Boxing.Business.Handlers.Predictions
{
    public class CancelPredictionHandler : BaseHandler, ICommandHandler<CancelPredictionRequest>
    {
        public async Task<Unit> HandleAsync(CancelPredictionRequest request)
        {
            // TODO: Ensure proper user sends this
            var prediction = await Context.Predictions.FindAsync(request.PredictionId);
            if (prediction == null)
            {
                throw new EntityDoesNotExistException("No such prediction");
            }
            // TODO: Result

            return Unit.Instance;
        }
    }
}
