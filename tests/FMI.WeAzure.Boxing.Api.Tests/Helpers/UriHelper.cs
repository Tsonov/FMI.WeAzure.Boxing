using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Api.Tests.Helpers
{
    internal static class UriHelper
    {
        public static Uri GetUri(string path, params Tuple<string, object>[] parameters)
        {
            // TODO: Extract host name
            UriBuilder builder = new UriBuilder(
                string.Format("http://localhost:55743/api/{0}", path));
            if (parameters.Any())
            {

                var query =
                    parameters.Aggregate(new StringBuilder(), (sbuild, param) =>
                    {
                        sbuild.AppendFormat("{0}={1}", Uri.EscapeDataString(param.Item1), Uri.EscapeDataString(param.Item2.ToString()));
                        sbuild.Append("&");
                        return sbuild;
                    })
                    .ToString();
                query = query.Remove(query.Length - 1, 1);
                builder.Query = query;
            }

            var uri = builder.Uri;
            return uri;
        }
    }
}
