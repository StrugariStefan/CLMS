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
            var emailService = new EmailService();
            var smsService = new SmsService();
            var notificationsController = new NotificationsController(emailService, smsService);

            //Act
            var actual = notificationsController.SendEmail(null);

            //Assert
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Valid_Email_Returns_Ok()
        {
            //Arrange
            var email = new Email();
            var mockEmailService = new Mock<IEmailService>();
            var mockSmsService = new Mock<ISmsService>();
            var notificationsController = new NotificationsController(mockEmailService.Object, mockSmsService.Object);

            //Act
            var actual = notificationsController.SendEmail(email);

            //Assert
            Assert.IsInstanceOfType(actual, typeof(OkResult));
        }

        [TestMethod]
        public void Null_Sms_Returns_BadRequest()
        {
            //Arrange
            var emailService = new EmailService();
            var smsService = new SmsService();
            var notificationsController = new NotificationsController(emailService, smsService);

            //Act
            var actual = notificationsController.SendSms(null);

            //Assert
            Assert.IsInstanceOfType(actual, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Valid_Sms_Returns_Ok()
        {
            //Arrange
            var sms = new Sms();
            var mockEmailService = new Mock<IEmailService>();
            var mockSmsService = new Mock<ISmsService>();
            var notificationsController = new NotificationsController(mockEmailService.Object, mockSmsService.Object);

            //Act
            var actual = notificationsController.SendSms(sms);

            //Assert
            Assert.IsInstanceOfType(actual, typeof(OkResult));
        }

    }
}