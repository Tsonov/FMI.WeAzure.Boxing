using FMI.WeAzure.Boxing.Contracts.Names;
using FMI.WeAzure.Boxing.Contracts.Requests.Boxers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Api.Tests.Helpers
{
    internal static class BoxerHelper
    {
        public static async Task<HttpResponseMessage> Create(string name, string biography)
        {
            using (var client = new HttpClient())
            {
                var uri = UriHelper.GetUri("boxers");
                client.DefaultRequestHeaders.Add(Headers.AdminAuthenticationHeader, AuthHelper.AdminKey);
                var data = new CreateBoxerRequest()
                {
                    Name = name,
                    Biography = biography
                };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                return await client.PostAsync(uri, content);
            }
        }
    }
}
