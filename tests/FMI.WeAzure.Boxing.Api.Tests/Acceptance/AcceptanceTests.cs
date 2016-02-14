using FMI.WeAzure.Boxing.Api.Tests.Helpers;
using FMI.WeAzure.Boxing.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FMI.WeAzure.Boxing.Api.Tests.Acceptance
{
    public class AcceptanceTests
    {
        private readonly Random rand = new Random();

        [Fact]
        [Trait("Category", "Acceptance")]
        public async Task MatchAndPredictionsWorkflow()
        {
            // Setup match first
            var boxers = await CreateRandomBoxers(2);
            var match = await CreateMatch(boxers[0], boxers[1]);

            // Now go on and use a new user to test some predictions
            string user = "Acceptance User " + Guid.NewGuid().ToString();
            string pass = "Acceptance Pass " + Guid.NewGuid().ToString();
            string fullName = "Acceptance user name";

            var createdUser = await SetupUser(user, pass, fullName);
            Assert.Equal(0, createdUser.Rating);
            Assert.Equal(user, createdUser.UserName);
            Assert.Equal(fullName, createdUser.FullName);

            var loginToken = await LoginUser(user, pass);

            // Now that we've logged in, create a prediction
            var addPredictionResp = await MatchesHelper.CreatePrediction(loginToken, match.Id, user, boxers[0].Id);
            Assert.True(addPredictionResp.IsSuccessStatusCode, "Invalid response status code " + addPredictionResp.StatusCode);

            // Close the match
            var finishMatchResp = await MatchesHelper.CloseMatch(match.Id, boxers[0].Id);
            Assert.True(finishMatchResp.IsSuccessStatusCode, "Invalid response status code " + finishMatchResp.StatusCode);

            // Assert our user just got a better rating
            var refreshUserResp = await UsersHelper.Get(user);
            Assert.True(refreshUserResp.IsSuccessStatusCode, "Invalid get user response with code " +refreshUserResp.StatusCode);
            var refreshedUser = await refreshUserResp.Content.ReadAsAsync<User>();
            Assert.Equal(1.0m, refreshedUser.Rating);
        }

        private async Task<User> SetupUser(string userName, string password, string fullName)
        {
            // Create an user to use
            var response = await UsersHelper.Register(userName, password, fullName);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
            return await response.Content.ReadAsAsync<User>();
        }

        private async Task<string> LoginUser(string username, string password)
        {

            // Do the actual login
            var loginResponse = await AuthHelper.Login(username, password);
            Assert.True(loginResponse.StatusCode == System.Net.HttpStatusCode.Created);

            return await loginResponse.Content.ReadAsAsync<string>();
        }

        private async Task<Match> CreateMatch(Boxer first, Boxer second)
        {
            string address = "Acceptance test value";
            string desc = "Acceptance test description";
            DateTime date = DateTime.UtcNow.AddDays(rand.Next(1, 10));

            var matchResponse = await MatchesHelper.Create(
                first.Id,
                second.Id,
                address,
                date,
                desc);

            Assert.True(matchResponse.StatusCode == System.Net.HttpStatusCode.Created, "Invalid response " + matchResponse.StatusCode);
            return await matchResponse.Content.ReadAsAsync<Match>();
        }

        private async Task<IList<Boxer>> CreateRandomBoxers(int count)
        {
            var boxers =
                Enumerable.Range(0, count)
                .Select(i => new { Name = "Test boxer " + Guid.NewGuid().ToString(), Biography = "Test bio " + i.ToString() })
                .ToArray();
            IList<Boxer> boxerResponses = new List<Boxer>();

            foreach (var boxerData in boxers)
            {
                var createResponse = await BoxerHelper.Create(boxerData.Name, boxerData.Biography);
                Assert.True(createResponse.StatusCode == System.Net.HttpStatusCode.Created);
                // TODO: Assert location header
                var boxer = await createResponse.Content.ReadAsAsync<Boxer>();

                Assert.NotNull(boxer);
                Assert.NotEqual(0, boxer.Id);
                Assert.Equal(boxerData.Name, boxer.Name);
                Assert.Equal(boxerData.Biography, boxer.Biography);

                boxerResponses.Add(boxer);
            }

            return boxerResponses;
        }
    }
}
