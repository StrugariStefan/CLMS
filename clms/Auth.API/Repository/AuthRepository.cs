using System;
using System.Collections.Generic;
using System.Net;
using Auth.API.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Auth.API.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private static readonly Dictionary<string, Guid> Tokens = new Dictionary<string, Guid>() { {"testToken", Guid.Parse("faa4c22e-7238-4fc7-b036-12ae1860d03f")} };

        public bool IsLoggedIn(string token, out Guid userId)
        {
            return Tokens.TryGetValue(token, out userId);
        }

        public string Login(LoginRequest loginRequest)
        {
            if (!IsUserRegistered(loginRequest, out var userId)) return null;

            var token = Guid.NewGuid().ToString();
            Tokens.Add(token, userId);
            return token;

        }

        public void Logout(LogoutRequest logoutRequest)
        {
            Tokens.Remove(logoutRequest.Token);
        }

        private static bool IsUserRegistered(LoginRequest loginRequest, out Guid userId)
        {
            var uri = $"http://localhost:5001/api/v1/users/registered";

            var client = new RestClient(uri);
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(loginRequest);

            var response = client.Execute(request);
            userId = JsonConvert.DeserializeObject<Guid>(response.Content);
            
            Console.WriteLine(userId);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
