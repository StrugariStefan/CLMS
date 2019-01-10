using System;
using System.Collections.Generic;
using Notifications.API.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace Notifications.API.Services
{
    public class EmailService : IEmailService
    {
        public void Send(Email email)
        {
            const string mailgun = "https://api.mailgun.net/v3";
            const string apiKey = "47632185b2e749776a5923adcc60ef50-9b463597-4a573798";
            const string domain = "sandboxa9fbb19463b04914982d4b83d9741e59.mailgun.org";
            var resource = $"{domain}/messages";
            var from = $"CLMS <clms@{domain}>";

            var client = new RestClient
            {
                BaseUrl = new Uri(mailgun), Authenticator = new HttpBasicAuthenticator("api", apiKey)
            };


            var request = new RestRequest
            {
                Method = Method.POST, Resource = resource
            };

            request.AddParameter("domain", domain, ParameterType.UrlSegment);
            
            request.AddParameter("from", from);
            AddRecipients(request, "to", email.To);
            AddRecipients(request, "cc", email.Cc);
            AddRecipients(request, "bcc", email.Bcc);

            request.AddParameter("subject", email.Subject);
            request.AddParameter("text", email.Body);

            client.Execute(request);
        }

        private static void AddRecipients(IRestRequest request, string recipientType, IEnumerable<string> recipients)
        {
            foreach (var recipient in recipients)
            {
                request.AddParameter(recipientType, recipient);
            }
        }
    }
}