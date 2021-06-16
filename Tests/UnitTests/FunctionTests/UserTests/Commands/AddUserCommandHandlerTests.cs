using Application.Functions.User.Commands;
using Application.Mapper;
using Application.Repos;
using Application.Services;
using Infrastructure.Mappers;
using NSubstitute;
using Xunit;
using Domain;
using Domain.Entities;
using Domain.Dtos;
using System;
using System.Threading;
using MediatR;
using System.Threading.Tasks;

namespace UnitTests.FunctionTests.UserTests.Commands
{
    public class AddUserCommandHandlerTests
    {
        private readonly IAuthenticationService _authenticationService = Substitute.For<IAuthenticationService>();
        private readonly IUserRepo _usersRepo = Substitute.For<IUserRepo>();
        private readonly IMapper _mapper;
        private AddUserCommandHandler _addUserHandler;
        public AddUserCommandHandlerTests()
        {
            _mapper = new Mapper();

            _addUserHandler = new(
                usersRepo: _usersRepo, 
                authenticationService: _authenticationService, 
                mapper: _mapper);
        }

        [Fact]
        public async Task Handle_ShouldReturnUnit()
        {
            //Given
            var command = new AddUserCommand
            {
                User = new UserToRegisterRequest
                {
                    Email = "test@test.com",
                    ConfirmEmail = "test@test.com",
                    UserName = "username",
                    FirstName = "first",
                    LastName = "last",
                    Password = "password",
                    ConfirmPassword = "password",
                    City = "warsaw",
                    BirthDate = DateTime.UtcNow.AddYears(-20)
                }
            };

            _usersRepo.SaveAllAsync().Returns(true);

            //When
            var result = await _addUserHandler.Handle(command, default(CancellationToken));

            //Then
            Assert.IsType(typeof(Unit), result);
        }
    }
}