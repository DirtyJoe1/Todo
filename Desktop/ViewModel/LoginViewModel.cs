using Desktop.Commands;
using Desktop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Todo.Entitites;

namespace Desktop.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(string e, string p)
        {
            _email = e;
            _password = p;
        }
        public Repository.Repository repository = new Repository.Repository();
        private string _email;
        private string _password;
        public string Email { get { return _email; } set { _email = value; OnPropertyChanged(); }}
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(); }}
        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(
                       p => true,
                       p => Login());
                }
                return _loginCommand;
            }
        }

        private async void Login()
        {
            var login = new LoginModelDto
            {
                Email = Email,
                Password = Password,

            };
            HttpResponseMessage response = await repository.PostUserLoginAsync(login);
        }
    }
}
