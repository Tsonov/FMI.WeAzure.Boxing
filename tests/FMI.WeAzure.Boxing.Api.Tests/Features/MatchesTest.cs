using FMI.WeAzure.Boxing.Api.Tests.Helpers;
using FMI.WeAzure.Boxing.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FMI.WeAzure.Boxing.Api.Tests.Features
{
    public class MatchesTest
    {
        private readonly Random rand = new Random();

        private async Task<IList<Boxer>> CreateRandomBoxers()
        {
            var boxers = new[]
            {
                new { Name = "Test boxer " + Guid.NewGuid().ToString(), Biography = "Test bio" },
                new { Name = "Test boxer " + Guid.NewGuid().ToString(), Biography = "Test bio 2" }
            };
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

        //private async Task<>

        [Fact]
        [Trait("Category", "Workflow")]
        public async Task CreateMatch_Succeeds()
        {
            // Create a few boxers to use first
            var boxerResponses = await CreateRandomBoxers();
            string address = "Test address value";
            string desc = "Test description";
            DateTime date = DateTime.UtcNow.AddDays(rand.Next(1, 10));

            var matchResponse = await MatchesHelper.Create(
                boxerResponses[0].Id,
                boxerResponses[1].Id,
                address,
                date,
                desc);

            Assert.True(matchResponse.StatusCode == System.Net.HttpStatusCode.Created, "Invalid response " + matchResponse.StatusCode);
            var data = await matchResponse.Content.ReadAsAsync<Match>();
            Assert.NotEqual(0, data.Id);
            Assert.Equal(boxerResponses[0].Id, data.FirstBoxer);
            Assert.Equal(boxerResponses[1].Id, data.SecondBoxer);
            Assert.Equal(address, data.Address);
            Assert.Equal(desc, data.Description);
            Assert.Equal(date, data.Date);
        }

        [Fact]
        [Trait("Category", "Workflow")]
        public async Task GetMatches_CorrectSortAndPagingResults()
        {
            var userName = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var fullName = "Test value";

            var registerResponse = await UsersHelper.Register(userName, password, fullName);
            Assert.True(registerResponse.IsSuccessStatusCode, "Could not register successfully");
            var loginResponse = await AuthHelper.Login(userName, password);
            Assert.True(loginResponse.StatusCode == System.Net.HttpStatusCode.Created);
            var userToken = await loginResponse.Content.ReadAsAsync<string>();

            // Create two matches to setup workflow test
            // Actual values are not that important since we might get other values
            // This is just to test that skip/take work
            var boxerResponses = await CreateRandomBoxers();
            for (int i = 0; i < 2; i++)
            {
                var matchResponse = await MatchesHelper.Create(
                    boxerResponses[0].Id,
                    boxerResponses[1].Id,
                    "Rand " + i,
                    DateTime.UtcNow.AddDays(1),
                    "Rand desc " + i);
                Assert.True(matchResponse.IsSuccessStatusCode, "Failed to create a match, status code is " + matchResponse.StatusCode);
            }

            var getResponse0 = await MatchesHelper.GetAll(userToken, 0, 1);
            var getResponse1 = await MatchesHelper.GetAll(userToken, 1, 1);
            Assert.True(getResponse0.IsSuccessStatusCode, "Unexpected get response " + getResponse0.StatusCode);
            Assert.True(getResponse1.IsSuccessStatusCode, "Unexpected get response " + getResponse1.StatusCode);

            var data0 = (await getResponse0.Content.ReadAsAsync<IEnumerable<Match>>()).Single();
            var data1 = (await getResponse1.Content.ReadAsAsync<IEnumerable<Match>>()).Single();

            // Should return two valid but different entries
            Assert.NotEqual(data0.Id, data1.Id);

        }
    }
}
