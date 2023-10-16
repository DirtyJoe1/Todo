using Desktop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Principal;
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
using Todo.Entitites;
using Windows.UI.Xaml.Media.Animation;

namespace Desktop.View
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    /// 
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var repository = new Repository.Repository();
            var login = new LoginModelDto
            {
                Email = Email.Text,
                Password = Password.Password,

            };
            HttpResponseMessage response = await repository.PostUserLoginAsync(login);
            if (response.IsSuccessStatusCode)
            {
                NavigationService?.Navigate(new MainEmptyPage(repository));
            }
            else
            {
                MessageBox.Show("Пользователя не существует");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RegistrationPage());
        }
    }
}
