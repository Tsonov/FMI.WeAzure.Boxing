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

        [Fact]
        [Trait("Category", "Workflow")]
        public async Task CreateMatch_Succeeds()
        {
            // Create a few boxers to use first
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

            var matchResponse = await MatchesHelper.Create(
                boxerResponses[0].Id,
                boxerResponses[1].Id,
                "Test address value",
                DateTime.UtcNow.AddDays(rand.Next(1, 10)),
                "Test description");

            Assert.True(matchResponse.StatusCode == System.Net.HttpStatusCode.Created, "Invalid response " + matchResponse.StatusCode);
        }
    }
}
