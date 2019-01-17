using System;
using Auth.API.Models;
using Auth.API.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Auth.Test
{
    [TestClass]
    public class AuthRepositoryTest
    {
        [TestMethod]
        public void Logout_RemovesToken()
        {
            //Arrange
            var authRepository = new AuthRepository();
            const string token = "testToken";
            var userId = Guid.Parse("faa4c22e-7238-4fc7-b036-12ae1860d03f");
            var logoutRequest = new LogoutRequest {Token = token};

            //Act
            authRepository.Logout(logoutRequest);

            //Assert
            Assert.IsFalse(authRepository.IsLoggedIn(token, out userId));
        }

        [TestMethod]
        public void GivenValidUserIdWhoIsLoggedIn_isLoggedIn_Returns_True()
        {
            var authRepository = new AuthRepository();
            var userId = Guid.Parse("faa4c22e-7238-4fc7-b036-12ae1860d03f");
            var actual = authRepository.IsLoggedIn("testToken", out userId);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void GivenInvalidToken_isLoggedIn_Returns_False()
        {
            var authRepository = new AuthRepository();
            var userId = Guid.Parse("faa4c22e-7238-4fc7-b036-12ae1860d03e");
            var actual = authRepository.IsLoggedIn("invalidToken", out userId);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void GivenRegisteredUser_Login_ReturnsToken()
        {
//            var authRepository = new AuthRepository();
//            var loginRequest = new LoginRequest {Email = "test@gmail.com", Password = "test!@#123"};
//            var token = authRepository.Login(loginRequest);
//
//            Assert.IsNotNull(token);
        }
    }
}