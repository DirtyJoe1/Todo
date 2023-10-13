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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new LoginPage());
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

            
            if (Validator.ValidateIsAnyEmpty(Name.Text, RegisterEmail.Text, RegisterPassword.Password))
            {
                MessageBox.Show("Some fields are empty");
            }
            else if (Validator.ValidateName(Name.Text) == false)
            {
                MessageBox.Show("Not valid name");
            }
            else if (Validator.ValidateEmail(RegisterEmail.Text) == false)
            {
                MessageBox.Show("Not valid email");
            }
            else if (Validator.ValidatePassword(RegisterPassword.Password) == false)
            {
                MessageBox.Show("Not valid password");
            }
            else if (RegisterPassword.Password != RegisterPasswordConfirm.Password)
            {
                MessageBox.Show("Passwords doesn't match");
            }
            else if (UserRepository.CheckEmail(RegisterEmail.Text))
            {
                UserRepository.AddUser(Name.Text, RegisterEmail.Text, RegisterPassword.Password);
                NavigationService?.Navigate(new MainEmptyPage((Name.Text)));
            }
            else
            {
                MessageBox.Show("Email is already taken");
            }
        }
    }
}
