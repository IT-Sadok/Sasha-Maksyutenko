
using BookingSystem.Application.DTOs;
using BookingSystem.Domain.Enums;
using BookingSystem.Infrastructure.Data;
using BookingSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using Shouldly;

namespace BookingSystem.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthService _authService;
        public AuthServiceTests()
        {
            var store=new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            _configurationMock = new Mock<IConfiguration>();
            _authService = new AuthService(_userManagerMock.Object, _configurationMock.Object);
           
            _configurationMock.Setup(x => x["Jwt:Secret"]).Returns("ThisIsASecretKeyForJwtTokenGeneration");
        }

        [Fact]
        public async Task RegisterAsync_WhenUserCreatedSuccessfully_ReturnsAuthResponse()
        {
            // Arrange
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
     .Callback<ApplicationUser, string>((user, _) => user.Id = Guid.NewGuid().ToString())
     .ReturnsAsync(IdentityResult.Success);
            var dto = new RegisterDTO
            {
                Name="Test",
                Email = "test@example.com",
                Password = "Password123!",
                Role=Role.Client
            };
            // Act
            var result = await _authService.RegisterAsync(dto);
            // Assert
            result.ShouldNotBeNull();
            result.JwtToken.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task RegisterAsync_WhenUserCreationFails_ThrowsException()
        {
            // Arrange
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Email already exists" }));
            var dto = new RegisterDTO
            {
                Name = "Test",
                Email = "test@example.com",
                Password = "Password123!",
                Role = Role.Client
            };
            // Act & Assert
            await Should.ThrowAsync<Exception>(async () => await _authService.RegisterAsync(dto));
        }

        [Fact]
        public async Task LoginAsync_WhenCredentialsAreValid_ReturnsAuthResponse()
        {
            // Arrange
            var user = new LoginDTO { Email = "test@example.com", Password = "Password123!" };
            var appUser = new ApplicationUser { Id = Guid.NewGuid().ToString(), Email = "test@example.com", Role = Role.Client };
            _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(appUser);
            _ = _userManagerMock.Setup(x => x.CheckPasswordAsync(null, user.Password)).ReturnsAsync(false);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(appUser, user.Password)).ReturnsAsync(true);
            // Act
            var result = await _authService.LoginAsync(user);
            // Assert
            result.ShouldNotBeNull();
            result.JwtToken.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task LoginAsync_WhenUserNotFound_ThrowsException()
        {
            // Arrange
            var user = new LoginDTO
            {
                Email = "nonexistent@example.com",
                Password = "Password123!"
            };
            _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(null as ApplicationUser);

            // Act & Assert
            await Should.ThrowAsync<Exception>(async () => await _authService.LoginAsync(user));
        }

        [Fact]
        public async Task LoginAsync_WhenPasswordIsInvalid_ThrowsException()
        {
            // Arrange
            var user = new LoginDTO
            {
                Email = "test@example.com",
                Password = "WrongPassword123!"
            };
            var appUser = new ApplicationUser { Id = Guid.NewGuid().ToString(), Email = "test@example.com", Role = Role.Client };
            _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(appUser);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(appUser, user.Password)).ReturnsAsync(false);

            // Act & Assert
            await Should.ThrowAsync<Exception>(async () => await _authService.LoginAsync(user));
        }
    }
}
