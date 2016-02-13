using FMI.WeAzure.Boxing.Business.Exceptions;
using FMI.WeAzure.Boxing.Business.Handlers.Authentication;
using FMI.WeAzure.Boxing.Business.Interfaces;
using FMI.WeAzure.Boxing.Contracts.Requests.Authentication;
using FMI.WeAzure.Boxing.Database;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FMI.WeAzure.Boxing.Business.Tests.Unit
{
    public class LoginTests
    {
        [Fact]
        public void NullContext_ShouldThrow()
        {
            var passServiceMock = Mock.Of<IPasswordService>();
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LoginHandler(passServiceMock, null);
            });
        }

        [Fact]
        public void NullService_ShouldThrow()
        {
            var mockContext = Mock.Of<BoxingDbContext>();
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LoginHandler(null, mockContext);
            });
        }

        [Theory]
        [InlineData(null)]
        [InlineData("does not exist")]
        public async Task InvalidUser_ShouldThrowNotFoundException(string username)
        {
            LoginRequest request = new LoginRequest()
            {
                UserName = username
            };
            var service = Mock.Of<IPasswordService>();
            var context = new Mock<BoxingDbContext>();
            context
                .SetupGet(ctx => ctx.Users)
                .Returns(() =>
                {
                    var mockSet = Mock.Of<DbSet<User>>();
                    Mock.Get(mockSet)
                        .Setup(m => m.FindAsync())
                        .ReturnsAsync(null);
                    return mockSet;
                });
            var handler = new LoginHandler(service, context.Object);
            try
            {
                await handler.HandleAsync(request);
            }
            catch (Exception)
            {

                throw;
            }

            await Assert.ThrowsAsync<EntityDoesNotExistException>(async () =>
            {
                await handler.HandleAsync(request);
            });
        }
    }
}
