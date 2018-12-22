using System;
using System.Collections.Generic;
using Notifications.API.Models;
using RestSharp;
using RestSharp.Authenticators;

namespace Notifications.API.Services
{
    public class EmailService : IEmailService
    {
        public void send(Email email)
        {
            string mailgun = "https://api.mailgun.net/v3";
            string apiKey = "47632185b2e749776a5923adcc60ef50-9b463597-4a573798";
            string domain = "sandboxa9fbb19463b04914982d4b83d9741e59.mailgun.org";
            string resource = $"{domain}/messages";
            string from = $"CLMS <clms@{domain}>";

            RestClient client = new RestClient();

            client.BaseUrl = new Uri(mailgun);
            client.Authenticator = new HttpBasicAuthenticator("api", apiKey);

            RestRequest request = new RestRequest();
            request.Method = Method.POST;

            request.Resource = resource;
            request.AddParameter("domain", domain, ParameterType.UrlSegment);
            
            request.AddParameter("from", from);
            addRecipients(request, "to", email.To);
            addRecipients(request, "cc", email.Cc);
            addRecipients(request, "bcc", email.Bcc);

            request.AddParameter("subject", email.Subject);
            request.AddParameter("text", email.Body);

            client.Execute(request);
        }

        private void addRecipients(RestRequest request, string recipientType, List<string> recipients)
        {
            foreach (string recipient in recipients)
            {
                request.AddParameter(recipientType, recipient);
            }
        }
    }
}