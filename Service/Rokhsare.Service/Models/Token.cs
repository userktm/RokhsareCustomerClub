using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rokhsare.Service.Models
{
    public static class Token
    {
        public static string NewToken()
        {
            var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789=";
            var random = new Random();
            var resultToken = new string(
               Enumerable.Repeat(allChar, 45)
               .Select(token => token[random.Next(token.Length)]).ToArray());

            string authToken = resultToken.ToString();

            return authToken;
        }

        public static string NewPassword()
        {
            var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789=";
            var random = new Random();
            var resultPassword = new string(
               Enumerable.Repeat(allChar, 6)
               .Select(pass => pass[random.Next(pass.Length)]).ToArray());

            string authPassword = resultPassword.ToString();

            return authPassword;
        }
    }
}