using FMI.WeAzure.Boxing.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Contracts.Requests.Users
{
    public class DeleteUserRequest : IRequest
    {
        public string UserName { get; set; }
    }
}
