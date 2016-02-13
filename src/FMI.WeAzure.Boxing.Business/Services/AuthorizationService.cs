using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Common;
using FMI.WeAzure.Boxing.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Business.Services
{
    public sealed class AuthorizationService : IAuthorizationService
    {
        private readonly BoxingDbContext context;

        public AuthorizationService(BoxingDbContext context)
        {
            Check.ThrowIfNull(context, "context");
            this.context = context;
        }

        public async Task<bool> ValidateAdminToken(string token)
        {
            var admin = await context.Administrators.FindAsync(token);
            return (admin != null && admin.Active);
        }

        public async Task<bool> ValidateLoginToken(string token)
        {
            var loginToken = await context.Logins.FindAsync(token);

            return (
                loginToken != null && 
                loginToken.Expired == false);
        }
    }
}
