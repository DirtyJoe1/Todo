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

namespace Desktop
{
    /// <summary>
    /// Логика взаимодействия для BaseWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        public BaseWindow()
        {
            InitializeComponent();
        }

        private void BaseFrame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
        }

        private void BaseFrame_Navigation(object sender, NavigationEventArgs e)
        {
            var fadeOutAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(0.75)),
                From = 0,
                To = 1,
                DecelerationRatio = 0.65
            };

            baseFrame.BeginAnimation(OpacityProperty, fadeOutAnimation);
        }
    }
}
