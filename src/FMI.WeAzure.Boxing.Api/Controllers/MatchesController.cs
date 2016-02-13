using FMI.WeAzure.Boxing.Api.Infrastructure.Filters;
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
        private readonly IRequestHandler<GetAllExpiredMatchesRequest, IEnumerable<Match>> getAllExpiredHandler;
        private readonly ICommandHandler<CreateMatchRequest> createMatchHandler;
        private readonly ICommandHandler<CancelMatchRequest> cancelMatchHandler;
        private readonly ICommandHandler<SetMatchResultRequest> setMatchResultHandler;
        private readonly ICommandHandler<AddNewPredictionRequest> addPredictionHandler;
        private readonly ICommandHandler<CancelPredictionRequest> cancelPredictionHandler;
        private readonly ICommandHandler<UpdatePredictionRequest> updatePredictionHandler;

        public MatchesController
            (
               IRequestHandler<GetAllMatchesRequest, IEnumerable<Match>> getAllHandler,
               IRequestHandler<GetAllExpiredMatchesRequest, IEnumerable<Match>> getAllExpiredHandler,
               ICommandHandler<CreateMatchRequest> createMatchHandler,
               ICommandHandler<CancelMatchRequest> cancelMatchHandler,
               ICommandHandler<SetMatchResultRequest> setMatchResultHandler,
               ICommandHandler<AddNewPredictionRequest> addPredictionHandler,
               ICommandHandler<CancelPredictionRequest> cancelPredictionHandler,
               ICommandHandler<UpdatePredictionRequest> updatePredictionHandler
            )
        {
            this.getAllHandler = getAllHandler;
            this.getAllExpiredHandler = getAllExpiredHandler;
            this.createMatchHandler = createMatchHandler;
            this.cancelMatchHandler = cancelMatchHandler;
            this.setMatchResultHandler = setMatchResultHandler;
            this.addPredictionHandler = addPredictionHandler;
            this.cancelPredictionHandler = cancelPredictionHandler;
            this.updatePredictionHandler = updatePredictionHandler;
        }

        [Route("")]
        [HttpGet]
        public async Task<IEnumerable<Match>> Get([FromUri] GetAllMatchesRequest request)
        {
            return await getAllHandler.HandleAsync(request);
        }

        [Route("expired")]
        [HttpGet]
        [AdminOnly]
        public async Task<IEnumerable<Match>> Get([FromUri] GetAllExpiredMatchesRequest request)
        {
            return await getAllExpiredHandler.HandleAsync(request);
        }

        [Route("")]
        [HttpPost]
        [AdminOnly]
        public async Task Post([FromBody] CreateMatchRequest request)
        {
            await createMatchHandler.HandleAsync(request);
        }

        [Route("{matchId:int}")]
        [HttpPatch]
        [AdminOnly]
        public async Task Patch([FromUri] int matchId, [FromBody] SetMatchResultRequest request)
        {
            request.MatchId = matchId;
            await setMatchResultHandler.HandleAsync(request);
        }

        [Route("{matchId:int}")]
        [HttpDelete]
        [AdminOnly]
        public async Task Delete([FromUri] CancelMatchRequest request)
        {
            await cancelMatchHandler.HandleAsync(request);
        }

        [Route("{matchId:int}/predictions")]
        [HttpPost]
        public async Task AddPredictions(int matchId, [FromBody] AddNewPredictionRequest request)
        {
            request.MatchId = matchId;
            await addPredictionHandler.HandleAsync(request);
        }

        [Route("{matchId:int}/predictions/{predictionId:int}")]
        [HttpPut]
        public async Task PutPrediction(int matchId, int predictionId, [FromBody] UpdatePredictionRequest request)
        {
            request.PredictionId = predictionId;
            await updatePredictionHandler.HandleAsync(request);
        }


        [Route("{matchId:int}/predictions")]
        [HttpDelete]
        public async Task CancelPrediction(CancelPredictionRequest request)
        {
            await cancelPredictionHandler.HandleAsync(request);
        }
    }
}