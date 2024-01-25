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

        private static string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        private static string Numbers = "0123456789";
        private static string SpecialCharacters = "!@#$%^&*()-_=+[]{}|;:'<>,.?/";

        public static string GenerateSecurePassword()
        {
            List<string> passwordGroups = new List<string>
        {
            GetRandomCharacters(UppercaseLetters, (new Random()).Next(3, 5)),
            GetRandomCharacters(LowercaseLetters, (new Random()).Next(3, 5)),
            GetRandomCharacters(Numbers, (new Random()).Next(2, 3)),
            GetRandomCharacters(SpecialCharacters,(new Random()).Next(2, 3))
        };

            string password = string.Join("", passwordGroups);
            password = ShuffleString(password);

            return password;
        }

        private static string GetRandomCharacters(string source, int count)
        {
            Random random = new Random();
            return new string(Enumerable.Range(0, count).Select(_ => source[random.Next(source.Length)]).ToArray());
        }

        private static string ShuffleString(string input)
        {
            char[] characters = input.ToCharArray();
            Random random = new Random();

            for (int i = characters.Length - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                char temp = characters[i];
                characters[i] = characters[j];
                characters[j] = temp;
            }

            return new string(characters);
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
