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
        public void Null_LoginRequest_Returns_BadRequest()
        {
            //Arrange
            var mockAuthRepository = new Mock<IAuthRepository>();
            var authController = new AuthController(mockAuthRepository.Object);

            //Act
            var expected = new BadRequestResult();
            var actual = authController.Login(null);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Valid_LoginRequest_Returns_Ok()
        {
            //Arrange
            var loginRequest = new LoginRequest();
            const string token = "testToken";

            var mockAuthRepository = new Mock<IAuthRepository>();
            mockAuthRepository.Setup(a => a.Login(loginRequest)
            ).Returns(token);

            var authController = new AuthController(mockAuthRepository.Object);

            //Act
            var expected = new OkObjectResult(token);
            var actual = authController.Login(loginRequest);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Unauthorized_LoginRequest_Returns_NotFound()
        {
            //Arrange
            var loginRequest = new LoginRequest();
            string token = null;

            var mockAuthRepository = new Mock<IAuthRepository>();
            mockAuthRepository.Setup(a => a.Login(loginRequest)
            ).Returns(token);

            var authController = new AuthController(mockAuthRepository.Object);

            //Act
            var expected = new NotFoundResult();
            var actual = authController.Login(loginRequest);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        public void Null_LogoutRequest_Returns_BadRequest()
        {
            //Arrange
            var mockAuthRepository = new Mock<IAuthRepository>();
            var authController = new AuthController(mockAuthRepository.Object);

            //Act
            var expected = new BadRequestResult();
            var actual = authController.Logout(null);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Valid_LogoutRequest_Returns_Ok()
        {
            //Arrange
            var logoutRequest = new LogoutRequest();
            var mockAuthRepository = new Mock<IAuthRepository>();
            mockAuthRepository.Setup(a => a.Logout(logoutRequest));

            var authController = new AuthController(mockAuthRepository.Object);

            //Act
            var expected = new OkResult();
            var actual = authController.Logout(logoutRequest);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
