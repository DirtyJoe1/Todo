using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModel
{
    public class RegistrationViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _email;
        public string Username { get { return _username; } set { _username = value; OnPropertyChanged(); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(); } }
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); } }
    }
}
