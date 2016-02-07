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
using FMI.WeAzure.Boxing.Business.Services;

namespace FMI.WeAzure.Boxing.Business.Handlers.Users
{
    public class CreateUserHandler : BaseHandler, ICommandHandler<CreateUserRequest>
    {
        private readonly IPasswordService passwordService;
        // TODO: DI
        public CreateUserHandler()
        {
            passwordService = new PasswordService();
        }

        public async Task<Unit> HandleAsync(CreateUserRequest request)
        {
            var password = passwordService.CreateHash(request.UserInfo.Password);
            var newEntity = new User()
            {
                FullName = request.UserInfo.UserName,
                Password = password
            };
            Context.Users.Add(newEntity);
            await Context.SaveChangesAsync();
            return Unit.Instance;
        }
    }
}
