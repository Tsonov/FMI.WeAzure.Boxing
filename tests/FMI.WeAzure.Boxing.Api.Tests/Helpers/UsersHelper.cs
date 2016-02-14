using FMI.WeAzure.Boxing.Contracts.Names;
using FMI.WeAzure.Boxing.Contracts.Requests.Authentication;
using FMI.WeAzure.Boxing.Contracts.Requests.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Api.Tests.Helpers
{
    internal static class UsersHelper
    {
        public static async Task<HttpResponseMessage> Register(string user, string password, string fullName)
        {
            using (var client = new HttpClient())
            {
                var uri = UriHelper.GetUri("users");
                var data = new CreateUserRequest()
                {
                    UserName = user,
                    Password = password,
                    FullName = fullName
                };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

                return await client.PostAsync(uri, content);
            }
        }

        public static async Task<HttpResponseMessage> Get(string user)
        {
            using (var client = new HttpClient())
            {
                var uri = UriHelper.GetUri("users/" + user);
                return await client.GetAsync(uri);
            }
        }

        public static async Task<HttpResponseMessage> Delete(string user)
        {
            using (var client = new HttpClient())
            {
                var uri = UriHelper.GetUri("users/" + user);
                client.DefaultRequestHeaders.Add(Headers.AdminAuthenticationHeader, AuthHelper.AdminKey);
                return await client.DeleteAsync(uri);
            }
        }
    }
}
