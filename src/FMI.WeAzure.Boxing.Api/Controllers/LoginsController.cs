using FMI.WeAzure.Boxing.Api.Infrastructure.Filters;
using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Requests.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FMI.WeAzure.Boxing.Api.Controllers
{
    [RoutePrefix("api/logins")]
    public class LoginsController : ApiController
    {
        private readonly IRequestHandler<LoginRequest, string> loginHandler;
        private readonly ICommandHandler<LogoutRequest> logoutHandler;

        public LoginsController
            (
                IRequestHandler<LoginRequest, string> loginHandler,
                ICommandHandler<LogoutRequest> logoutHandler
            )
        {
            this.loginHandler = loginHandler;
            this.logoutHandler = logoutHandler;
        }
        
        [Route("")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] LoginRequest request)
        {
            var token = await loginHandler.HandleAsync(request);
            return Request.CreateResponse(HttpStatusCode.Created, token);
        }

        [Route("{token}")]
        [HttpDelete]
        [LoggedIn]
        public async Task Delete([FromUri] LogoutRequest request)
        {
            await logoutHandler.HandleAsync(request);
        }
    }
}