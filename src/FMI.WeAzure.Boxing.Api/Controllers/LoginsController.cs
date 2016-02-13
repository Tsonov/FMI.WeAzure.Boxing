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
        
        [HttpPost]
        public async Task<string> Post([FromBody] LoginRequest request)
        {
            return await loginHandler.HandleAsync(request);
        }

        [HttpDelete]
        public async Task Delete([FromBody] LogoutRequest request)
        {
            await logoutHandler.HandleAsync(request);
        }
    }
}