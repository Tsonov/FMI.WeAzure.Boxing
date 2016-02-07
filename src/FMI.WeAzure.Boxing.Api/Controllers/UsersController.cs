﻿using FMI.WeAzure.Boxing.Business.Handlers.Users;
using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Requests.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FMI.WeAzure.Boxing.Api.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/<controller>
        public async Task<IEnumerable<User>> Get(GetAllUsersRequest request)
        {
            return await (new GetAllUsersHandler()).HandleAsync(request);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var user = AwesomeDataRepository.Users.SingleOrDefault(x => x.Id == id);
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
       
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            // TODO: Needs admin validation
            var item = AwesomeDataRepository.Users.SingleOrDefault(u => u.Id == id);
            if (item != null)
            {
                AwesomeDataRepository.Users.Remove(item);
            }
        }
    }
}