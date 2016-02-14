using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Requests.Boxers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FMI.WeAzure.Boxing.Api.Controllers
{
    [RoutePrefix("api/boxers")]
    public class BoxersController : ApiController
    {
        private readonly IRequestHandler<CreateBoxerRequest, Boxer> createBoxerHandler;

        public BoxersController
            (
                IRequestHandler<CreateBoxerRequest, Boxer> createBoxerHandler
            )
        {
            this.createBoxerHandler = createBoxerHandler;
        }

        [Route("")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] CreateBoxerRequest request)
        {
            var boxer = await createBoxerHandler.HandleAsync(request);
            return Request.CreateResponse<Boxer>(HttpStatusCode.Created, boxer);
        }
    }
}