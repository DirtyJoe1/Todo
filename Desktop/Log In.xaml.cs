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
using System.Windows.Shapes;
using Todo.Entitites;

namespace Desktop
{
    /// <summary>
    /// Логика взаимодействия для Log_In.xaml
    /// </summary>
    public partial class Log_In : Window
    {
        public Log_In()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validator.ValidateEmail(Email.Text) == false)
            {
                MessageBox.Show("Not valid email");
            }
            else if (Validator.ValidatePassword(Password.Password) == false)
            {
                MessageBox.Show("Not valid password");
            }

            else if (UserRepository.CheckUser(Email.Text, Password.Password))
            {
                var MainEmptyWindow = new MainEmpty();
                MainEmptyWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var registration = new Registration();
            registration.Show();
            this.Close();
        }
    }
}
