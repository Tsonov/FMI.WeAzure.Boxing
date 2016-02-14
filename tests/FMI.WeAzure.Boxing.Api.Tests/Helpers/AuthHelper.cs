using FMI.WeAzure.Boxing.Contracts.Names;
using FMI.WeAzure.Boxing.Contracts.Requests.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Api.Tests.Helpers
{
    internal static class AuthHelper
    {
        public static async Task<HttpResponseMessage> Login(string user, string password)
        {
            using (var client = new HttpClient())
            {
                var uri = UriHelper.GetUri("logins");
                var data = new LoginRequest()
                {
                    UserName = user,
                    Password = password,
                };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                return await client.PostAsync(uri, content);
            }
        }

        public static async Task<HttpResponseMessage> Logout(string token)
        {
            using (var client = new HttpClient())
            {
                var uri = UriHelper.GetUri("logins/" + token);
                client.DefaultRequestHeaders.Add(Headers.UserTokenHeader, token);
                return await client.DeleteAsync(uri);
            }
        }

        public static readonly string AdminKey = "AdminKey1";
    }
}
