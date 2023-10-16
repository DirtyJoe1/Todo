using Desktop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    /// Логика взаимодействия для MainEmptyPage.xaml
    /// </summary>
    public partial class MainEmptyPage : Page
    {
        Repository.Repository _repository;
        public MainEmptyPage(Repository.Repository repository)
        {
            InitializeComponent();
            _repository = repository;
            SetUsername();
        }
        public async void SetUsername()
        {
            string content = await _repository.GetUser();
            UsernameLable.Content = content;
        }

        private void CreateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage(_repository));
        }
    }
}
