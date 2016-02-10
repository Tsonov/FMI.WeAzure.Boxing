using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Dto;
using FMI.WeAzure.Boxing.Contracts.Requests.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FMI.WeAzure.Boxing.Api.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IRequestHandler<GetAllUsersRequest, IEnumerable<User>> getAllHandler;
        private readonly IRequestHandler<GetUserRequest, User> getSingleHandler;
        private readonly ICommandHandler<DeleteUserRequest> deleteUserHandler;
        private readonly ICommandHandler<CreateUserRequest> createUserHandler;

        public UsersController
            (
                IRequestHandler<GetAllUsersRequest, IEnumerable<User>> getAllHandler,
                IRequestHandler<GetUserRequest, User> getSingleHandler,
                ICommandHandler<DeleteUserRequest> deleteUserHandler,
                ICommandHandler<CreateUserRequest> createUserHandler
            )
        {
            this.getAllHandler = getAllHandler;
            this.getSingleHandler = getSingleHandler;
            this.deleteUserHandler = deleteUserHandler;
            this.createUserHandler = createUserHandler;
        }

        public async Task<IEnumerable<User>> Get(GetAllUsersRequest request)
        {
            return await getAllHandler.HandleAsync(request);
        }

        public async Task<User> Get(GetUserRequest request)
        {
            return await getSingleHandler.HandleAsync(request);
        }

        public async Task Post(CreateUserRequest request)
        {
            await createUserHandler.HandleAsync(request);
        }
       
        public async Task Delete(DeleteUserRequest request)
        {
            await deleteUserHandler.HandleAsync(request);
        }
    }
}