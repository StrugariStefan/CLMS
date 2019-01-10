using System;
using System.Collections.Generic;
using System.Net;
using Auth.API.Models;
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

        public string Login(LoginRequest loginRequest)
        {
            if (!IsUserRegistered(loginRequest)) return null;

            var token = Guid.NewGuid().ToString();
            Tokens.Add(token);
            return token;

        }

        public void Logout(LogoutRequest logoutRequest)
        {
            Tokens.Remove(logoutRequest.Token);
        }

        private static bool IsUserRegistered(LoginRequest loginRequest)
        {
            var uri = $"http://localhost:5001/api/v1/users/registered";

            var client = new RestClient(uri);
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(loginRequest);

            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
