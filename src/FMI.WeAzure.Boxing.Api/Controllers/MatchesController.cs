using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Requests.Matches;
using FMI.WeAzure.Boxing.Contracts.Requests.Predictions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FMI.WeAzure.Boxing.Api.Controllers
{
    [RoutePrefix("api/matches")]
    public class MatchesController : ApiController
    {
        private readonly IRequestHandler<GetAllMatchesRequest, IEnumerable<Match>> getAllHandler;
        private readonly ICommandHandler<CreateMatchRequest> createMatchHandler;
        private readonly ICommandHandler<CancelMatchRequest> cancelMatchHandler;
        private readonly ICommandHandler<AddNewPredictionRequest> addPredictionHandler;
        private readonly ICommandHandler<CancelPredictionRequest> cancelPredictionHandler;
        private readonly ICommandHandler<UpdatePredictionRequest> updatePredictionHandler;

        public MatchesController
            (
               IRequestHandler<GetAllMatchesRequest, IEnumerable<Match>> getAllHandler,
               ICommandHandler<CreateMatchRequest> createMatchHandler,
               ICommandHandler<CancelMatchRequest> cancelMatchHandler,
               ICommandHandler<AddNewPredictionRequest> addPredictionHandler,
               ICommandHandler<CancelPredictionRequest> cancelPredictionHandler,
               ICommandHandler<UpdatePredictionRequest> updatePredictionHandler
            )
        {
            this.getAllHandler = getAllHandler;
            this.createMatchHandler = createMatchHandler;
            this.cancelMatchHandler = cancelMatchHandler;
            this.addPredictionHandler = addPredictionHandler;
            this.cancelPredictionHandler = cancelPredictionHandler;
            this.updatePredictionHandler = updatePredictionHandler;
        }

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Match>> Get(GetAllMatchesRequest request)
        {
            return await getAllHandler.HandleAsync(request);
        }

        [Route("")]
        [HttpPost]
        public async Task Post([FromBody]CreateMatchRequest request)
        {
            await createMatchHandler.HandleAsync(request);
        }

        [Route("")]
        [HttpDelete]
        public async Task Delete(CancelMatchRequest request)
        {
            await cancelMatchHandler.HandleAsync(request);
        }

        [Route("api/matches/{id}/predictions")]
        [HttpPost]
        public async Task AddPredictions([FromBody] AddNewPredictionRequest request)
        {
            await addPredictionHandler.HandleAsync(request);
        }

        [Route("api/matches/{id}/predictions")]
        [HttpPut]
        public async Task PutPrediction(UpdatePredictionRequest request)
        {
            await updatePredictionHandler.HandleAsync(request);
        }


        [Route("api/matches/{id}/predictions")]
        [HttpDelete]
        public async Task CancelPrediction(CancelPredictionRequest request)
        {
            await cancelPredictionHandler.HandleAsync(request);
        }
    }
}