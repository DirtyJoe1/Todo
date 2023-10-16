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
using Todo.Entitites;

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

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registration = new RegistrationModelDto()
            {
                Name = RegisterEmail.Text,
                Email = RegisterEmail.Text,
                Password = RegisterPassword.Password,
            };
            var repository = new Repository.Repository();
            var response = await repository.PostUserRegistrationAsync(registration);
            if (response.IsSuccessStatusCode)
            {
                NavigationService?.Navigate(new MainEmptyPage(repository));
            }
            else
            {
                MessageBox.Show("Пользователь уже зарегестрирован");
            }
        }
    }
}
