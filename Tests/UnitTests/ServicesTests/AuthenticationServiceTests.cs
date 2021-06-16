using NSubstitute;
using Microsoft.Extensions.Configuration;
using Xunit;
using Infrastructure.Services;
using System.Threading.Tasks;
using System;

namespace UnitTests.ServicesTests
{
    public class AuthenticationServiceTests
    {
        private const string tokenDescriptor = "test_token_dasdjlasjdljaslkjdlasldajsdljaslkjdlkajslkdjlaskjkl";
        private readonly AuthenticationService _authService;
        private readonly IConfiguration _config = Substitute.For<IConfiguration>();

        public AuthenticationServiceTests()
        {
            _authService = new AuthenticationService(_config);
        }

        [Fact]
        public async Task CreateToken_ShouldCreateToken()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userName = "janKowalski";
            var email = "jankowalski@gmail.com";
            _config.GetSection("AppSettings:Token").Value.Returns(tokenDescriptor);
            
            // Act
            var token = await _authService.CreateToken(userId, userName, email);

            // Asert
            Assert.IsType<string>(token);
        }

        [Fact]
        public void CreatePasswordHash_ShouldHave32And64Length()
        {
            // Arrange
            byte[] passwordHash;
            byte[] passwordSalt;
            var password = "Password123!$$";

            // Act
            _authService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Assert
            Assert.Equal<int>(32, passwordHash.Length);
            Assert.Equal<int>(64, passwordSalt.Length);

        }

        [Fact]
        public void VerifyPasswordHash_ShouldVerify()
        {
            // Arrange
            byte[] passwordHash;
            byte[] passwordSalt;
            var password = "Password123!$$";
            _authService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Act
            var isVerified = _authService.VerifyPasswordHash(password, passwordHash, passwordSalt);

            // Assert
            Assert.True(isVerified);
        }

        [Fact]
        public void VerifyPasswordHash_ShouldNotVerify()
        {
            // Arrange
            byte[] passwordHash;
            byte[] passwordSalt;
            var password = "Password123!$$";
            var wrongPassword = "123..321..123";
            _authService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            // Act
            var isVerified = _authService.VerifyPasswordHash(wrongPassword, passwordHash, passwordSalt);

            // Assert
            Assert.False(isVerified);
        }

    }
}