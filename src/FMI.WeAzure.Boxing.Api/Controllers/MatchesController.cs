using FMI.WeAzure.Boxing.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FMI.WeAzure.Boxing.Api.Controllers
{
    public class MatchesController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Match> Get([FromBody] string searchString = "", 
            [FromUri] int skip = 0, 
            [FromUri] int take = 10)
        {
            return null;
        }

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        public Match Post([FromBody]Match match)
        {
            match.Id = AwesomeDataRepository.Matches.Select(x => x.Id).DefaultIfEmpty(0).Max() + 1;
            AwesomeDataRepository.Matches.Add(match);
            // TODO: Id
            return match;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Match value)
        {
            var existing = AwesomeDataRepository.Matches.SingleOrDefault(m => m.Id == id);
            if (existing == null)
            {
                // TODO: Handles
                throw new Exception("Invalid id for match to update");
            }
            AwesomeDataRepository.Matches.Remove(existing);
            AwesomeDataRepository.Matches.Add(value);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            // TODO: Check for admin
            var match = AwesomeDataRepository.Matches.SingleOrDefault(x => x.Id == id);
            if (match == null)
            {
                // TODO: Better code
                throw new Exception("Invalid match");
            }

            // TODO: Predictions
            AwesomeDataRepository.Matches.Remove(match);
        }

        [HttpPost]
        [Route("api/matches/{id}/predictions")]
        public void AddPredictions([FromUri] int id, [FromBody] Prediction prediction)
        {
            var match = AwesomeDataRepository.Matches.SingleOrDefault(m => m.Id == id);
            if (match == null)
            {
                throw new Exception("Invalid match id");
                // TODO fix
            }
            // TODO: Use user as well
            match.Predictions.Add(prediction);
        }

        [HttpPut]
        [Route("api/matches/{id}/predictions")]
        public void PutPrediction([FromUri] int id, [FromBody] Prediction prediction)
        {
            // TODO: Make a "is deactivated field"
            // TODO: Implement
        }
    }
}