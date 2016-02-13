using FMI.WeAzure.Boxing.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace FMI.WeAzure.Boxing.Api.Infrastructure.Filters
{
    public class EntityDoesNotExistExceptionFilter : ExceptionFilterAttribute
    {
            public override void OnException(HttpActionExecutedContext actionExecutedContext)
            {
                if (actionExecutedContext.Exception is EntityDoesNotExistException)
                {
                    actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(actionExecutedContext.Exception.Message));
                }
            }
    }
}