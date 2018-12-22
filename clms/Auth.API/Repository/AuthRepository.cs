using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using RestSharp;

namespace Auth.API.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private static readonly List<string> Tokens = new List<string>() { "testToken" };

        public bool IsLoggedIn(string token)
        {
            return Tokens.Contains(token);
        }

        public string Login(string email, string password)
        {
            if (IsUserRegistered(email, password))
            {
                string token = Guid.NewGuid().ToString();
                Tokens.Add(token);
                return token;
            }

            return null;
        }

        private bool IsUserRegistered(string email, string password)
        {
            string uri = $"http://localhost:5001/api/v1/users/registered";

            RestRequest request = new RestRequest();
            request.Method = Method.POST;

            return new HttpClient().GetAsync(uri).Result.StatusCode == HttpStatusCode.OK;
        }
    }
}
