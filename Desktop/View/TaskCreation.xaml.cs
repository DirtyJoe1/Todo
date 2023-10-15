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
    /// Логика взаимодействия для TaskCreation.xaml
    /// </summary>
    public partial class TaskCreation : Page
    {
        MainPage _mainPage;
        DateTime date;
        public TaskCreation(MainPage main)
        {
            InitializeComponent();
            _mainPage = main;
            for (int hour = 0; hour < 24; hour++)
            {
                hourComboBox.Items.Add(hour.ToString("D2"));
            }
            for (int minute = 0; minute < 60; minute += 5)
            {
                minuteComboBox.Items.Add(minute.ToString("D2"));
            }
            hourComboBox.SelectionChanged += TimeChanged;
            minuteComboBox.SelectionChanged += TimeChanged;
        }
        private void TimeChanged(object sender, SelectionChangedEventArgs e)
        {
            var pickedDate = datePicker.SelectedDate.Value;
            if (hourComboBox.SelectedItem != null && minuteComboBox.SelectedItem != null)
            {

                int selectedHour = int.Parse(hourComboBox.SelectedItem.ToString());
                int selectedMinute = int.Parse(minuteComboBox.SelectedItem.ToString());
                date = new DateTime(DateTime.Today.Year, pickedDate.Month, pickedDate.Day, selectedHour, selectedMinute, 0);
            }
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            TaskModel newTask = new TaskModel()
            {
                TaskDateTime = date,
                Title = Title.Text,
                Description = Description.Text,
                IsCompleted = false,
                VisibleTime = date.Hour.ToString(),
                Color = new SolidColorBrush(Colors.White),
                Category = new TaskCategory()
                {
                    CategoryName = Category.Text,
                    Color = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256))),
                }
            };

            _mainPage.tasks.Add(newTask);
            if (!_mainPage.taskCategories.Any(t => t.CategoryName == newTask.Category.CategoryName))
            {
                _mainPage.taskCategories.Add(newTask.Category);
            }
            _mainPage.UpdateLists();
            NavigationService?.Navigate(_mainPage);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
