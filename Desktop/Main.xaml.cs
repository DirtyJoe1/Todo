using Desktop.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private List<SolidColorBrush> colors;
        private ObservableCollection<TaskModel> tasks;
        private ObservableCollection<TaskCategories> taskCategories;
        public Main(string username)
        {
            InitializeComponent();
            UserNameTextBlock.Text = username;

            colors = new List<SolidColorBrush> 
            { 
                new SolidColorBrush(Colors.Lime), 
                new SolidColorBrush(Colors.Pink), 
                new SolidColorBrush(Colors.Purple), 
                new SolidColorBrush(Colors.Red) };

            taskCategories = new ObservableCollection<TaskCategories> 
            { 
                new TaskCategories { CategoryName = "Дом", Color = colors[0] }, 
                new TaskCategories { CategoryName = "Работа", Color = colors[1] }, 
                new TaskCategories { CategoryName = "Учеба", Color = colors[2] }, 
                new TaskCategories { CategoryName = "Отдых", Color = colors[3] } 
            };
            MenuList.ItemsSource = taskCategories;

            var taskDateTime = DateTime.Now;
            tasks = new ObservableCollection<TaskModel>
            {
                new TaskModel {Title = "Заголовок", Description = "Go fishing with Setphan", TaskDateTime = taskDateTime, DisplayTime = taskDateTime.ToString(), IsCompleted = false, Color = Brushes.White},
                new TaskModel {Title = "Заголовок", Description = "Go fishing with Setphan", TaskDateTime = taskDateTime, DisplayTime = taskDateTime.ToString(), IsCompleted = false, Color = Brushes.White},
                new TaskModel {Title = "Заголовок", Description = "Read the book Zlatan", TaskDateTime = taskDateTime, DisplayTime = taskDateTime.ToString(), IsCompleted = false, Color = Brushes.White},
                new TaskModel {Title = "Заголовок", Description = "Meet according with design team", TaskDateTime = taskDateTime, DisplayTime = taskDateTime.ToString(), IsCompleted = false, Color = Brushes.White},
                new TaskModel {Title = "Заголовок", Description = "Meet according with design team", TaskDateTime = taskDateTime, DisplayTime = taskDateTime.ToString(), IsCompleted = false, Color = Brushes.White},
                new TaskModel {Title = "Заголовок", Description = "Meet according with design team", TaskDateTime = taskDateTime, DisplayTime = taskDateTime.ToString(), IsCompleted = false, Color = Brushes.White},
            };
            TasksList.ItemsSource = tasks;
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var task = (TaskModel)TasksList.SelectedItem;
            tasks.Remove(task);
        }

        private void DoneButton_OnClick(object sender, RoutedEventArgs e)
        {
            var task = (TaskModel)TasksList.SelectedItem;
            if (task.IsCompleted == false)
            {
                tasks.Remove(task);
                task.IsCompleted = true;
                task.Color = new SolidColorBrush(Colors.Red);
                tasks.Add(task);
                TaskFullContent.Visibility = Visibility.Hidden;
            }
            else
            {
                tasks.Remove(task);
                task.IsCompleted = false;
                task.Color = new SolidColorBrush(Colors.White);
                tasks.Insert(0, task);
                TaskFullContent.Visibility = Visibility.Visible;
            }
        }

        private void TasksList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TasksList.SelectedItem is TaskModel task)
            {
                TitleTextBlock.Text = task.Title;
                TimeTextBlock.Text = task.TaskDateTime.Date.ToString("dd MMMM");
                DateTextBlock.Text = task.TaskDateTime.ToShortTimeString();
                ContentTextBlock.Text = task.Description;
                TaskFullContent.Visibility = Visibility.Visible;
            }
            else
            {
                TaskFullContent.Visibility = Visibility.Hidden;
            }
        }
    }
}
