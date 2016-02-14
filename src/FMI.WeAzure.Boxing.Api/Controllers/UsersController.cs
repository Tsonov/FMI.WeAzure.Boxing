using FMI.WeAzure.Boxing.Api.Infrastructure.Filters;
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
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IRequestHandler<GetAllUsersRequest, IEnumerable<User>> getAllHandler;
        private readonly IRequestHandler<GetUserRequest, User> getSingleHandler;
        private readonly ICommandHandler<DeleteUserRequest> deleteUserHandler;
        private readonly IRequestHandler<CreateUserRequest, User> createUserHandler;

        public UsersController
            (
                IRequestHandler<GetAllUsersRequest, IEnumerable<User>> getAllHandler,
                IRequestHandler<GetUserRequest, User> getSingleHandler,
                ICommandHandler<DeleteUserRequest> deleteUserHandler,
                IRequestHandler<CreateUserRequest, User> createUserHandler
            )
        {
            this.getAllHandler = getAllHandler;
            this.getSingleHandler = getSingleHandler;
            this.deleteUserHandler = deleteUserHandler;
            this.createUserHandler = createUserHandler;
        }

        [Route("")]
        public async Task<IEnumerable<User>> Get([FromUri] GetAllUsersRequest request)
        {
            // TODO: Investigate why doesn't it get initialized...
            if (request == null)
            {
                request = new GetAllUsersRequest();
            }
            return await getAllHandler.HandleAsync(request);
        }

        [Route("{username}")]
        [HttpGet]
        public async Task<User> Get([FromUri] GetUserRequest request)
        {
            return await getSingleHandler.HandleAsync(request);
        }

        [Route("")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] CreateUserRequest request)
        {
            var userData = await createUserHandler.HandleAsync(request);
            return Request.CreateResponse(HttpStatusCode.Created, userData);
        }

        [Route("{username}")]
        [HttpDelete]
        [AdminOnly]
        public async Task Delete([FromUri] DeleteUserRequest request)
        {
            await deleteUserHandler.HandleAsync(request);
        }
    }
}