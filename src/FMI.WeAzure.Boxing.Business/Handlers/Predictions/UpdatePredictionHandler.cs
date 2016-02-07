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
            // TODO: Switch result with what the user posted


            await Context.SaveChangesAsync();
            return Unit.Instance;
        }
    }
}
