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
using System.Windows.Shapes;

namespace Desktop
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            Manager.window = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Validator.ValidateName(Name.Text) == false)
            {
                MessageBox.Show("Not valid name");
            }
            if(Validator.ValidateEmail(RegisterEmail.Text) == false)
            {
                MessageBox.Show("Not valid name");
            }
            if(Validator.ValidatePassword(RegisterPassword.Password) == false)
            {
                MessageBox.Show("Not valid password");
            }
            if(RegisterPassword.Password != RegisterPasswordConfirm.Password)
            {
                MessageBox.Show("Passwords doesn't match");
            }
            else
            {
                var MainEmptyWindow = new MainEmpty();
                MainEmptyWindow.Show();
                this.Close();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var LoginWindow = new Log_In();
            LoginWindow.Show();
            this.Close();
        }
    }
}
