using FMI.WeAzure.Boxing.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FMI.WeAzure.Boxing.Api.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<User> Get()
        {
            return AwesomeDataRepository.Users;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var user = AwesomeDataRepository.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        // POST api/<controller>
        public void Post([FromBody]User user)
        {
            if (user == null)
            {
                throw new Exception("Invalid null user");
            }
            AwesomeDataRepository.Users.Add(user);
        }

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}