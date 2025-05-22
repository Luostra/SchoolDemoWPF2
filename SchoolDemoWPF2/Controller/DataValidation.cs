using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SchoolDemoWPF2.Controller
{
    static class DataValidation
    {
        public static bool IsValidEmail(string email)
        {
            // Стандартное регулярное выражение для email
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase) || string.IsNullOrEmpty(email);

        }
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Пример для номеров РФ
            string pattern = @"^8\d{10}$";
            return Regex.IsMatch(phoneNumber, pattern) || string.IsNullOrEmpty(phoneNumber);
        }
        public static bool IsValidFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return false;

            // Паттерн: русские/английские буквы, начинается с заглавной, может содержать дефис
            string pattern = @"^[А-ЯЁA-Z][а-яёa-z-]+(?:\s[А-ЯЁA-Z][а-яёa-z-]+){1,2}$";

            return Regex.IsMatch(fullName, pattern);
        }
        public static bool IsValidClassName(string className)
        {
            if (string.IsNullOrWhiteSpace(className))
                return false;

            // Паттерн: цифра от 1 до 11, необязательный пробел, одна русская буква
            string pattern = @"^(1[0-1]|[1-9])\s?[А-ЯЁ]$";

            return Regex.IsMatch(className.Trim(), pattern, RegexOptions.IgnoreCase);
        }
    }
}
