using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
            if (IsUserRegistered(loginRequest))
            {
                string token = Guid.NewGuid().ToString();
                Tokens.Add(token);
                return token;
            }

            return null;
        }

        private bool IsUserRegistered(LoginRequest loginRequest)
        {
            string uri = $"http://localhost:5001/api/v1/users/registered";

            RestClient client = new RestClient(uri);
            RestRequest request = new RestRequest(Method.POST);
            request.AddJsonBody(loginRequest);

            IRestResponse response = client.Execute(request);
            Console.WriteLine("Auth Repository: ---------------------");
            Console.WriteLine(response);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
