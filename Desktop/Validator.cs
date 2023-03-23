using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop
{
    internal class Validator
    {
        public static bool ValidatePassword(string password)
        {
            return password.Length >= 6;
        }
        public static bool ValidateEmail(string email)
        {
            return (Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"));
        }
        public static bool ValidateName(string name)
        {
            return name.Length >= 3;
        }
        public static bool ValidateIsAnyEmpty(string name, string email, string password)
        {
            return string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password);
        }
        public static bool ValidateIsAnyEmpty(string email, string password)
        {
            return string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password);
        }
    }
}