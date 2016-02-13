using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Api.Tests.Helpers
{
    internal static class UriHelper
    {
        public static Uri GetUri(string path)
        {
            // TODO: Extract
            return new Uri("http://localhost:55743/api/" + path);
        }
    }
}
