﻿using FMI.WeAzure.Boxing.Contracts.Names;
using FMI.WeAzure.Boxing.Contracts.Requests.Matches;
using FMI.WeAzure.Boxing.Contracts.Requests.Predictions;
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
                var uri = UriHelper.GetUri("matches");
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add(Headers.AdminAuthenticationHeader, AuthHelper.AdminKey);

                return await client.PostAsync(uri, content);
            }
        }

        public static async Task<HttpResponseMessage> GetAll(string token, int skip = 0, int take = 15, string search = "")
        {
            using (var client = new HttpClient())
            {
                var uri = UriHelper.GetUri("matches",
                    Tuple.Create("skip", (object)skip),
                    Tuple.Create("take", (object)take),
                    Tuple.Create("search", (object)search));
                client.DefaultRequestHeaders.Add(Headers.UserTokenHeader, token);

                return await client.GetAsync(uri);
            }
        }

        public static async Task<HttpResponseMessage> CreatePrediction(string token, int matchId, string user, int winner)
        {
            using (var client = new HttpClient())
            {
                var uri = UriHelper.GetUri(String.Format("matches/{0}/predictions", matchId));
                var data = new AddNewPredictionRequest()
                {
                    MadeByUser = user,
                    PredictedWinner = winner
                };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add(Headers.UserTokenHeader, token);

                return await client.PostAsync(uri, content);
            }
        }

        public static async Task<HttpResponseMessage> CloseMatch(int matchId, int winner)
        {
            using (var client = new HttpClient())
            {
                var uri = UriHelper.GetUri(String.Format("matches/{0}", matchId));
                var data = new SetMatchResultRequest()
                {
                    Winner = winner
                };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add(Headers.AdminAuthenticationHeader, AuthHelper.AdminKey);

                return await client.PostAsync(uri, content);
            }
        }
    }
}
