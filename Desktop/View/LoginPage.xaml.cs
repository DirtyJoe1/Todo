using Desktop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.UI.Xaml.Media.Animation;

namespace Desktop.View
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (Validator.ValidateIsAnyEmpty(Email.Text, Password.Password))
            {
                MessageBox.Show("Some fields are empty");
            }
            else if (Validator.ValidateEmail(Email.Text) == false)
            {
                MessageBox.Show("Not valid email");
            }
            else if (Validator.ValidatePassword(Password.Password) == false)
            {
                MessageBox.Show("Not valid password");
            }
            else if (UserRepository.CheckUser(Email.Text, Password.Password))
            {
                NavigationService?.Navigate(new MainEmptyPage(UserRepository.CurrentUserName(Email.Text)));
            }
            else
            {
                MessageBox.Show("User not exist");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegistrationPage());
        }
    }
}
