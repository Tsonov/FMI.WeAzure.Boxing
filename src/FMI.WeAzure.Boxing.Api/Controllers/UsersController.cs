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
        public IEnumerable<User> Get(GetUsersRequest request)
        {
            return null;
            //var result =  AwesomeDataRepository.Users.Skip(skip).Take(take);
            //Func<User, object> orderer;
            //if (orderCol == UserOrderColumn.FullName)
            //{
            //    orderer = (x => x.UserName);
            //}
            //else
            //{
            //    orderer = (x => x.Id); // TODO: Fix DTOs
            //}
            //result = order == Order.Ascending ? result.OrderBy(orderer) : result.OrderByDescending(orderer);

            //return result;
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

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

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