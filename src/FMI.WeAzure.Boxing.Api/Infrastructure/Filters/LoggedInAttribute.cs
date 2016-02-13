using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Names;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace FMI.WeAzure.Boxing.Api.Infrastructure.Filters
{
    public sealed class LoggedInAttribute : AuthorizeAttribute
    {
        public override async Task OnAuthorizationAsync
            (
                HttpActionContext actionContext,
                CancellationToken cancellationToken
            )
        {
            IAuthorizationService service =
                actionContext.Request.GetDependencyScope().GetService(typeof(IAuthorizationService)) as IAuthorizationService;
            Debug.Assert(service != null, "Auth service was not injected correctly");

            var headers = actionContext.Request.Headers;
            IEnumerable<string> values;
            var token = string.Empty;
            // TODO: Use authorization header?
            if (headers.TryGetValues(Headers.UserTokenHeader, out values))
            {
                token = values.FirstOrDefault();
                var valid = await service.ValidateLoginToken(token);
                if (valid)
                {
                    return;
                }
            }

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            await base.OnAuthorizationAsync(actionContext, cancellationToken);
        }
    }
}