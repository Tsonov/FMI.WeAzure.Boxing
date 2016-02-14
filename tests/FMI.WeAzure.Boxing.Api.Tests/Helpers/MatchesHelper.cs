using FMI.WeAzure.Boxing.Contracts.Names;
using FMI.WeAzure.Boxing.Contracts.Requests.Matches;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Api.Tests.Helpers
{
    internal static class MatchesHelper
    {
        public static async Task<HttpResponseMessage> Create(int firstBoxer, int secondBoxer, string address, DateTime time, string description)
        {
            var request = new CreateMatchRequest()
            {
                FirstBoxer = firstBoxer,
                SecondBoxer = secondBoxer,
                Address = address,
                Date = time,
                Description = description
            };

            using (var client = new HttpClient())
            {
                var uri = UriHelper.GetUri("/matches");
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add(Headers.AdminAuthenticationHeader, AuthHelper.AdminKey);

                return await client.PostAsync(uri, content);
            }
        }
    }
}
