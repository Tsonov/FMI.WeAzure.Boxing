using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Requests.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Business.Exceptions;

namespace FMI.WeAzure.Boxing.Business.Handlers.Users
{
    public class DeleteUserHandler : BaseHandler, ICommandHandler<DeleteUserRequest>
    {
        public async Task<Unit> HandleAsync(DeleteUserRequest request)
        {
            var entity = await Context.Users.FindAsync(request.UserName);
            if (entity == null)
            {
                throw new EntityDoesNotExistException("No such user");
            }
            Context.Users.Remove(entity);
            await Context.SaveChangesAsync();
            return Unit.Instance;
        }
    }
}
