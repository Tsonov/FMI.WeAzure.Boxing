using FMI.WeAzure.Boxing.Business.Exceptions;
using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Business.Services;
using FMI.WeAzure.Boxing.Common;
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
        private static readonly RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
        private readonly IPasswordService passwordService;

        public LoginHandler(IPasswordService passwordService, BoxingDbContext context) : base(context)
        {
            Check.ThrowIfNull(passwordService, "passwordService", "Provided password service can not be null");
            this.passwordService = passwordService;
        }

        public async Task<string> HandleAsync(LoginRequest request)
        {
            var user = await Context.Users.FindAsync(request.UserName);
            if (user == null)
            {
                throw new EntityDoesNotExistException("User for login is invalid");
            }

            if (passwordService.ValidatePassword(request.Password, user.Password) == false)
            {
                throw new WrongPasswordException("Invalid password provided");
            }

            byte[] tokenRng = new byte[16];
            string token;
            bool alreadyExists;
            do
            {
                crypto.GetBytes(tokenRng);
                token = 
                    Convert.ToBase64String(tokenRng) // Needs some massaging to get it to be url-safe
                    .TrimEnd('=')
                    .Replace('+', '-')
                    .Replace('/', '_');

                alreadyExists = (await Context.Logins.FindAsync(token)) != null;
            } while (alreadyExists);

            Login newLogin = new Login()
            {
                Token = token,
                IssuedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(1),
                ForUser = user,
                LogoutAt = null
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
