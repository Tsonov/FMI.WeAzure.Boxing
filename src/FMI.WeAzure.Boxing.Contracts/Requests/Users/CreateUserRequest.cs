using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Requests.Users
{
    public class CreateUserRequest : IRequest<User>
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullName { get; set; }
    }
}
