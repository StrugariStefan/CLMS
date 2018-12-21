using System;
using System.Security.Cryptography;

namespace Users.API.Helpers
{
    public static class Utils
    {
        public static string Hash(this String password)
        {
            byte[] hash = SHA256.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            string hashedPassword = System.Text.Encoding.UTF8.GetString(hash);

            return hashedPassword;
        }
    }
}