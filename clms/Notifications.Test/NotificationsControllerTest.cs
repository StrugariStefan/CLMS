using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Notifications.API.Controllers;
using Notifications.API.Models;
using Notifications.API.Services;

namespace Notifications.Test
{
    [TestClass]
    public class NotificationsControllerTest
    {
        [TestMethod]
        public void Null_Email_Returns_BadRequest()
        {
            //Arrange
            var mockEmailService = new Mock<IEmailService>();
            var mockSmsService = new Mock<ISmsService>();
            var notificationsController = new NotificationsController(mockEmailService.Object, mockSmsService.Object);
            
            //Act
            var expected = new BadRequestResult();
            var actual = notificationsController.SendEmail(null);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Valid_Email_Returns_Ok()
        {
            var email = new Email();

            //Arrange
            var mockEmailService = new Mock<IEmailService>();
            mockEmailService.Setup(n => n.Send(
                It.Is<Email>(e => e == email)));

            var mockSmsService = new Mock<ISmsService>();
            var notificationsController = new NotificationsController(mockEmailService.Object, mockSmsService.Object);

            //Act
            var expected = new OkResult();
            var actual = notificationsController.SendEmail(email);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Null_Sms_Returns_BadRequest()
        {
            //Arrange
            var mockEmailService = new Mock<IEmailService>();
            var mockSmsService = new Mock<ISmsService>();
            var notificationsController = new NotificationsController(mockEmailService.Object, mockSmsService.Object);

            //Act
            var expected = new BadRequestResult();
            var actual = notificationsController.SendSms(null);

            Console.WriteLine(expected);
            Console.WriteLine(actual);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Valid_Sms_Returns_Ok()
        {
            var sms = new Sms();

            //Arrange
            var mockEmailService = new Mock<IEmailService>();
            var mockSmsService = new Mock<ISmsService>();
            mockSmsService.Setup(n => n.Send(
                It.Is<Sms>(s => s == sms)));

            var notificationsController = new NotificationsController(mockEmailService.Object, mockSmsService.Object);

            //Act
            var expected = new OkResult();
            var actual = notificationsController.SendSms(sms);
            
            //Assert
            Assert.AreEqual(expected, actual);
        }

    }
}