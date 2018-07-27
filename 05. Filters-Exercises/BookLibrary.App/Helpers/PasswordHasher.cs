﻿using System.Linq;
using System.Security.Cryptography;

namespace BookLibrary.App.Helpers
{
    public class PasswordHasher
    {
        public static string GetPasswordHash(string password)
        {
            var sha256 = new SHA256Managed();
            return string.Join(""
                , sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
                .Select(b => b.ToString("x2")));
        }
    }
}
