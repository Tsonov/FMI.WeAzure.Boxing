using FMI.WeAzure.Boxing.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FMI.WeAzure.Boxing.Api.Controllers
{
    public class LoginsController : ApiController
    {
        // POST api/<controller>
        public Login Post(string value)
        {
            var login = new Login() { Id = AwesomeDataRepository.Logins.Max(l => l.Id) + 1, AuthToken = "Test"};
            AwesomeDataRepository.Logins.Add(login);
            return login;
        }
        
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            var login = AwesomeDataRepository.Logins.SingleOrDefault(l => l.Id == id);
            if (login != null)
            {
                AwesomeDataRepository.Logins.Remove(login);
            }
        }
    }
}