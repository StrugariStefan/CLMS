using Auth.API.Controllers;
using Auth.API.Models;
using Auth.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Auth.Test
{
    [TestClass]
    public class AuthControllerTest
    {
        [TestMethod]
        public void Null_LoginRequest_Returns_BadRequest()
        {
            //Arrange
            var authRepository = new AuthRepository();
            var authController = new AuthController(authRepository);

            //Act
            var actual = authController.Login(null);

            //Assert
            Assert.IsInstanceOfType(actual.Result, typeof(BadRequestResult));
        }


        [TestMethod]
        public void Valid_LoginRequest_Returns_Ok()
        {
            //Arrange
            var loginRequest = new LoginRequest();
            var mockAuthRepository = new Mock<IAuthRepository>();
            mockAuthRepository.Setup(a => a.Login(loginRequest)).Returns("testToken");
            var authController = new AuthController(mockAuthRepository.Object);

            //Act
            var actual = authController.Login(loginRequest);

            //Assert
            Assert.IsInstanceOfType(actual.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void Unauthorized_LoginRequest_Returns_NotFound()
        {
            //Arrange
            var loginRequest = new LoginRequest();
            string token = null;

            var mockAuthRepository = new Mock<IAuthRepository>();
            mockAuthRepository.Setup(a => a.Login(loginRequest)).Returns(token);

            var authController = new AuthController(mockAuthRepository.Object);

            //Act
            var actual = authController.Login(loginRequest);

            //Assert
            Assert.IsInstanceOfType(actual.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Null_LogoutRequest_Returns_BadRequest()
        {
            //Arrange
            var mockAuthRepository = new Mock<IAuthRepository>();
            var authController = new AuthController(mockAuthRepository.Object);

            //Act
            var actual = authController.Logout(null);

            //Assert
            Assert.IsInstanceOfType(actual.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Valid_LogoutRequest_Returns_Ok()
        {
            //Arrange
            var logoutRequest = new LogoutRequest();
            var mockAuthRepository = new Mock<IAuthRepository>();

            var authController = new AuthController(mockAuthRepository.Object);

            //Act
            var actual = authController.Logout(logoutRequest);

            //Assert
            Assert.IsInstanceOfType(actual.Result, typeof(OkResult));
        }
    }
}
