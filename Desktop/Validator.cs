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
            if (password.Length < 6) return false;
            else return true;
        }
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            else return (Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"));
        }
        public static bool ValidateName(string name)
        {
            if(name.Length< 3) return false;
            else return true;
        }
    }
}
