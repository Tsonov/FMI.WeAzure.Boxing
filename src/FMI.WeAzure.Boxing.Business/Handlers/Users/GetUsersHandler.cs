using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Requests.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Business.Handlers.Users
{
    public class GetUsersHandler : IRequestHandler<GetAllUsersRequest, IEnumerable<User>>
    {

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> HandleAsync(GetAllUsersRequest request)
        {
            return Task.FromResult(new List<User>().AsEnumerable());
        }
    }
}
