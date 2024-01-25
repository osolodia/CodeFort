using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeFort.Generator
{
    public static class SecurePasswordGenerator
    {
        private const string SpecialChars = "!@#$%^&*()-_=+[]{}|;:'\",.<>/?";

        public static string? GenerateSecurePassword(int length = 12)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+[]{}|;:'\",.<>/?";

            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                char[] securePassword = new char[length];

                for (int i = 0; i < length; i++)
                {
                    securePassword[i] = validChars[randomBytes[i] % validChars.Length];
                }

                return securePassword.ToString();
            }
        }

        public static bool IsPasswordSecure(string password)
        {
            // Проверка безопасности пароля (пример)
            return password.Length >= 8
                   && password.Any(char.IsLower)
                   && password.Any(char.IsUpper)
                   && password.Any(char.IsDigit)
                   && password.Any(c => SpecialChars.Contains(c));
        }
    }
}
