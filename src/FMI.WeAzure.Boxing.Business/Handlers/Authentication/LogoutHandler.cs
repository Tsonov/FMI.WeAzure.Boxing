using FMI.WeAzure.Boxing.Business.Exceptions;
using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts;
using FMI.WeAzure.Boxing.Contracts.Requests.Authentication;
using FMI.WeAzure.Boxing.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Business.Handlers.Authentication
{
    public class LogoutHandler : BaseHandler, ICommandHandler<LogoutRequest>
    {
        public async Task<Unit> HandleAsync(LogoutRequest request)
        {
            var login = await Context.Logins.FindAsync(request.Token);
            if (login == null)
            {
                throw new EntityDoesNotExistException("Login was not found");
            }
            login.LogoutAt = DateTime.UtcNow;
            await Context.SaveChangesAsync();
            return Unit.Instance;
        }
    }
}
