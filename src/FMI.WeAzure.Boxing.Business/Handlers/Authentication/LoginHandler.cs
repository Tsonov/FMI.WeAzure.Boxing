using FMI.WeAzure.Boxing.Business.Exceptions;
using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Requests.Authentication;
using FMI.WeAzure.Boxing.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Business.Handlers.Authentication
{
    public class LoginHandler : BaseHandler, IRequestHandler<LoginRequest, string>
    {
        public async Task<string> HandleAsync(LoginRequest request)
        {
            // TODO: Proper password  handling
            var user = await Context.Users.FindAsync(request.UserInfo.Password);
            if (user == null)
            {
                throw new EntityDoesNotExistException("User for login is invalid");
            }
            Login newLogin = new Login()
            {
                Token = "TODO",
                IssuedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(1),
                ForUser = user,
            };
            Context.Logins.Add(newLogin);
            await Context.SaveChangesAsync();
            return newLogin.Token;
        }
    }
}
