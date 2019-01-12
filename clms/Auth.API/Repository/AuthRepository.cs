using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using Auth.API.Context;
using Auth.API.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Auth.API.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private static readonly List<string> Tokens = new List<string>() { "testToken" };
        private readonly ApplicationContext _context;

        public AuthRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool IsLoggedIn(string token, out string userId)
        {
            try
            {
                userId = _context.Tokens.FirstOrDefault(t => t.ActualToken == token).UserId.ToString();
            }
            catch (NullReferenceException e)
            {
                userId = null;
            }
            
            return Tokens.Contains(token);
        }

        public string Login(LoginRequest loginRequest)
        {
            if (!IsUserRegistered(loginRequest, out var userId)) return null;

            Token token = new Token(Guid.Parse(userId));
            _context.Tokens.Add(token);
            _context.SaveChanges();
            //var token = Guid.NewGuid().ToString();
            //Tokens.Add(token);
            Tokens.Add(token.ActualToken);
            return token.ActualToken;

        }

        public void Logout(LogoutRequest logoutRequest)
        {
            var token = _context.Tokens.First(c => c.ActualToken == logoutRequest.Token);
            _context.Tokens.Remove(token);
            _context.SaveChanges();
            Tokens.Remove(logoutRequest.Token);
        }

        private static bool IsUserRegistered(LoginRequest loginRequest, out string userId)
        {
            var uri = $"http://localhost:5001/api/v1/users/registered";

            var client = new RestClient(uri);
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(loginRequest);

            var response = client.Execute(request);
            userId = JsonConvert.DeserializeObject<string>(response.Content);
            
            Console.WriteLine(userId);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
