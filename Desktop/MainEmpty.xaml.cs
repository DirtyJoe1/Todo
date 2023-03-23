using System;
using System.Collections.Generic;
using System.Configuration;
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
using Desktop.Repository;

namespace Desktop
{
    /// <summary>
    /// Логика взаимодействия для MainEmpty.xaml
    /// </summary>
    public partial class MainEmpty : Window
    {
        private string username;
        public MainEmpty(string _username)
        {
            InitializeComponent();
            username = _username;
            Name.Text = username;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var main = new Main(username);
            main.Show();
            this.Close();
        }
    }
}
