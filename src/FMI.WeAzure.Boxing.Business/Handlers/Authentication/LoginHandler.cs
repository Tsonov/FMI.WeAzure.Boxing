using FMI.WeAzure.Boxing.Business.Exceptions;
using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Requests.Authentication;
using FMI.WeAzure.Boxing.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Business.Handlers.Authentication
{
    public class LoginHandler : BaseHandler, IRequestHandler<LoginRequest, string>
    {
        private readonly RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();

        public async Task<string> HandleAsync(LoginRequest request)
        {

            // TODO: Proper password handling
            var user = await Context.Users.FindAsync(request.UserInfo.Password);
            if (user == null)
            {
                throw new EntityDoesNotExistException("User for login is invalid");
            }
            byte[] tokenRng = new byte[16];
            string token;
            bool alreadyExists;
            do
            {
                crypto.GetBytes(tokenRng);
                token = Convert.ToBase64String(tokenRng);
                alreadyExists = (await Context.Logins.FindAsync(token)) != null;
            } while (alreadyExists);

            Login newLogin = new Login()
            {
                Token = token,
                IssuedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(1),
                ForUser = user,
            };
            Context.Logins.Add(newLogin);
            await Context.SaveChangesAsync();
            return newLogin.Token;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                crypto.Dispose();
            }
        }
    }
}
