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
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FMI.WeAzure.Boxing.Api.Infrastructure.Filters
{
    public sealed class AdminOnlyAttribute : AuthorizationFilterAttribute
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
            if (headers.TryGetValues(Headers.AdminAuthenticationHeader, out values))
            {
                token = values.FirstOrDefault();
                var valid = await service.ValidateAdminToken(token);
                if (valid)
                {
                    await base.OnAuthorizationAsync(actionContext, cancellationToken);
                    return;
                }
            }

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}