using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Requests.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto = FMI.WeAzure.Boxing.Contracts.Dto;

namespace FMI.WeAzure.Boxing.Business.Handlers.Users
{
    public class GetUserHandler : BaseHandler, IRequestHandler<GetUserRequest, Dto.User>
    {
        public async Task<Dto.User> HandleAsync(GetUserRequest request)
        {
            var dbUser = await Context.Users.FindAsync(request.UserName);
            return new Dto.User()
            {
                UserName = dbUser.Username
            };
        }
    }
}
