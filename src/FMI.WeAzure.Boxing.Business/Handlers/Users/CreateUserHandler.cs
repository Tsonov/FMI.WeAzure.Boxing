using FMI.WeAzure.Boxing.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Database;
using Dto = FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Requests.Users;

namespace FMI.WeAzure.Boxing.Business.Handlers.Users
{
    public class CreateUserHandler : BaseHandler, ICommandHandler<CreateUserRequest>
    {
        public async Task<Unit> HandleAsync(CreateUserRequest request)
        {
            var newEntity = new User()
            {
                FullName = request.UserInfo.UserName,
            };
            Context.Users.Add(newEntity);
            await Context.SaveChangesAsync();
            return Unit.Instance;
        }
    }
}
