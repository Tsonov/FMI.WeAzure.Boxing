using FMI.WeAzure.Boxing.Api.Tests.Helpers;
using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Requests.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;

namespace FMI.WeAzure.Boxing.Api.Tests.Features
{
    public class UsersTests
    {
        [Fact]
        [Trait("Category", "Workflow")]
        public async Task Register_SucceedsAndReturnsInfo()
        {
            var userName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var fullName = "Test value";

            var response = await UsersHelper.Register(userName, password, fullName);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        [Trait("Category", "Workflow")]
        public async Task GetUsers_ReturnsInfo()
        {
            using (var client = new HttpClient())
            {
                var uri = UriHelper.GetUri("/users");
                var response = await client.GetAsync(uri);
                Assert.True(response.IsSuccessStatusCode);
                var content = await response.Content.ReadAsAsync<IEnumerable<User>>();

                Assert.NotNull(content);
                Assert.All(content, item =>
                {
                    Assert.NotNull(item.UserName);
                    Assert.NotEmpty(item.UserName);
                    Assert.NotNull(item.FullName);
                });
            }
        }
    }
}
