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
            var serviceMock = new Mock<IPasswordService>();
            serviceMock
                .Setup(s =>
                        s.ValidatePassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            var setMock = new Mock<DbSet<User>>()
                    .SetupData(new User[] { new User() { Username = "Different name" } });
            var contextMock = new Mock<BoxingDbContext>();
            contextMock.Setup(c => c.Users).Returns(setMock.Object);


            var service = serviceMock.Object;
            var handler = new LoginHandler(service, contextMock.Object);

            await Assert.ThrowsAsync<EntityDoesNotExistException>(async () =>
            {
                await handler.HandleAsync(request);
            });
        }

        [Theory]
        [InlineData("user", "password", "hash")]
        public async Task ValidUser_ShouldGenerateNewLogin(string user, string password, string passwordhash)
        {
            var usersData = new List<User>()
            {
                new User() { Username = user, Password = passwordhash },
            };
            var loginsData = new List<Login>();

            var serviceMock = new Mock<IPasswordService>();
            serviceMock
                .Setup(s =>
                        s.ValidatePassword(password, passwordhash))
                .Returns(true);

            var userSetMock = new Mock<DbSet<User>>()
                    .SetupData(usersData, objs => usersData.Find(u => u.Username == (string)objs.First()));
            var loginsSetMock = new Mock<DbSet<Login>>()
                .SetupData(loginsData, objs => loginsData.Find(u => u.Token == (string)objs.First()));

            var contextMock = new Mock<BoxingDbContext>();
            contextMock.Setup(c => c.Users).Returns(userSetMock.Object);
            contextMock.Setup(c => c.Logins).Returns(loginsSetMock.Object);

            var request = new LoginRequest()
            {
                UserName = user,
                Password = password
            };
            var handler = new LoginHandler(serviceMock.Object, contextMock.Object);
            var result = await handler.HandleAsync(request);

            Assert.NotNull(result);
            Assert.True(result.Length > 0);
            Assert.True(loginsData.Count == 1);
            Assert.True(loginsData[0].Token == result);
            Assert.True(loginsData[0].ForUser.Username == user);
            serviceMock.Verify(s => s.ValidatePassword(password, passwordhash), Times.Once);
        }

        //[Theory]
        //[InlineData("password1", "password2", "password3")]
        //public async Task InvalidPassword_ShoultThrowWrongPasswordException(IEnumerable<string> passwords)
        //{
        //    var serviceMock = new Mock<IPasswordService>();
        //    serviceMock
        //        .Setup(s =>
        //                s.ValidatePassword(It.IsIn(passwords), It.IsAny<string>()))
        //        .Returns(true);
        //}
    }
}
