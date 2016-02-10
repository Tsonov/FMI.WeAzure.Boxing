using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Requests.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FMI.WeAzure.Boxing.Api.Controllers
{
    public class MatchesController : ApiController
    {
        private readonly IRequestHandler<GetAllMatchesRequest, IEnumerable<Match>> getAllHandler;
        private readonly ICommandHandler<CreateMatchRequest> createMatchHandler;
        private readonly ICommandHandler<CancelMatchRequest> cancelMatchHandler;

        public MatchesController
            (
               IRequestHandler<GetAllMatchesRequest, IEnumerable<Match>> getAllHandler,
               ICommandHandler<CreateMatchRequest> createMatchHandler,
               ICommandHandler<CancelMatchRequest> cancelMatchHandler
            )
        {
            this.getAllHandler = getAllHandler;
            this.createMatchHandler = createMatchHandler;
            this.cancelMatchHandler = cancelMatchHandler;
        }

        public async Task<IEnumerable<Match>> Get(GetAllMatchesRequest request)
        {
            return await getAllHandler.HandleAsync(request);
        }

        public async Task Post(CreateMatchRequest request)
        {
            await createMatchHandler.HandleAsync(request);
        }

        public async Task Delete(CancelMatchRequest request)
        {
            await cancelMatchHandler.HandleAsync(request);
        }

        [HttpPost]
        [Route("api/matches/{id}/predictions")]
        public void AddPredictions([FromUri] int id, [FromBody] Prediction prediction)
        {
            var match = AwesomeDataRepository.Matches.SingleOrDefault(m => m.Id == id);
            if (match == null)
            {
                throw new Exception("Invalid match id");
                // TODO fix
            }
            // TODO: Use user as well
            match.Predictions.Add(prediction);
        }

        [HttpPut]
        [Route("api/matches/{id}/predictions")]
        public void PutPrediction([FromUri] int id, [FromBody] Prediction prediction)
        {
        }
    }
}