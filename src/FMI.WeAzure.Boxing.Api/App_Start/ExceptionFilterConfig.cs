using FMI.WeAzure.Boxing.Api.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FMI.WeAzure.Boxing.Api
{
    public static class ExceptionFilterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new EntityDoesNotExistExceptionFilter());
        }
    }
}