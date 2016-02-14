using FMI.WeAzure.Boxing.Api.Tests.Helpers;
using FMI.WeAzure.Boxing.Contracts.Requests.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FMI.WeAzure.Boxing.Api.Tests.Features
{
    public class LoginTests
    {
        [Fact]
        [Trait("Category", "Workflow")]
        public async Task Login_SucceedsAndReturnsToken()
        {
            // TODO: Create a test db with ready-available data to avoid re-registering
            var userName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var fullName = "Test value";

            var registerResponse = await UsersHelper.Register(userName, password, fullName);
            Assert.True(registerResponse.IsSuccessStatusCode, "Could not register successfully");

            // Do the actual login
            var loginResponse = await AuthHelper.Login(userName, password);
            Assert.True(loginResponse.StatusCode == System.Net.HttpStatusCode.Created);

            var token = await loginResponse.Content.ReadAsAsync<string>();
            System.Diagnostics.Debug.WriteLine(token);
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        [Trait("Category", "Workflow")]
        public async Task Login_LogoutWorks()
        {
            var userName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var fullName = "Test value";

            var registerResponse = await UsersHelper.Register(userName, password, fullName);
            Assert.True(registerResponse.IsSuccessStatusCode, "Could not register successfully, code was " + registerResponse.StatusCode);
            var loginResponse = await AuthHelper.Login(userName, password);
            Assert.True(loginResponse.IsSuccessStatusCode, "Could not login successfully, code was " + loginResponse.StatusCode);
            var token = await loginResponse.Content.ReadAsAsync<string>();

            var logoutResponse = await AuthHelper.Logout(token);
            Assert.True(logoutResponse.IsSuccessStatusCode, "Could not logout, code was " + logoutResponse.StatusCode);
        }
    }
}
