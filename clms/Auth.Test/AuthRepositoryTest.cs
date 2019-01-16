using System;
using Auth.API.Models;
using Auth.API.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Auth.Test
{
    [TestClass]
    public class AuthRepositoryTest
    {
        private readonly IAuthRepository _authRepository = new AuthRepository();

        [TestMethod]
        public void Logout_RemovesToken()
        {
            //Arrange
            var logoutRequest = new LogoutRequest {Token = "testToken"};

            //Act
            _authRepository.Logout(logoutRequest);

            //Assert
        }

        [TestMethod]
        public void GivenValidUserId_isLoggedIn_Returns_True()
        {
            var userId = Guid.Parse("faa4c22e-7238-4fc7-b036-12ae1860d03f");
            var actual = _authRepository.IsLoggedIn("testToken", out userId);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void GivenInvalidUserId_isLoggedIn_Returns_False()
        {
            var userId = Guid.Parse("faa4c22e-7238-4fc7-b036-12ae1860d03e");
            var actual = _authRepository.IsLoggedIn("testToken", out userId);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void GivenRegisteredUser_Login_ReturnsToken()
        {
            var loginRequest = new LoginRequest {Email = "test@gmail.com", Password = "test!@#123"};
            var token = _authRepository.Login(loginRequest);

            Assert.IsNotNull(token);
        }
    }
}