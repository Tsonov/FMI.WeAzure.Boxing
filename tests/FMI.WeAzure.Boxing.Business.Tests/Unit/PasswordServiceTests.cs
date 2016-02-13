using FMI.WeAzure.Boxing.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FMI.WeAzure.Boxing.Business.Tests.Unit
{
    public class PasswordServiceTests
    {
        [Theory]
        [InlineData("test-password")]
        [InlineData("longer-password-with-more-symbols-and-so-on")]
        [InlineData("contains-:-special-symbol")]
        public void ValidPasswords_GenerateAndValidate(string password)
        {
            var service = new PasswordService();

            var hash = service.CreateHash(password);
            Assert.NotNull(hash);
            Assert.True(hash.Length > 0);

            var validates = service.ValidatePassword(password, hash);
            Assert.True(validates);
        }
    }
}
