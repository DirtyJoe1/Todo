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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Todo.Entitites;

namespace Desktop.View
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>();
        public ObservableCollection<TaskCategory> taskCategories = new ObservableCollection<TaskCategory>();
        public ObservableCollection<TaskModel> completedTasks = new ObservableCollection<TaskModel>();
        public ObservableCollection<TaskCategory> completedtaskCategories = new ObservableCollection<TaskCategory>();
        public bool isInHistory = false;
        public MainPage(string username)
        {
            InitializeComponent();
            UsernameLable.Content = username;
            UpdateLists();
        }
        public void UpdateLists()
        {
            TasksList.ItemsSource = tasks;
            MenuList.ItemsSource = taskCategories;
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TaskCreation(this));
        }
        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var task = (TaskModel)TasksList.SelectedItem;
            if (!isInHistory)
            {
                tasks.Remove(task);
                if (!tasks.Any(t => t.Category.CategoryName == task.Category.CategoryName))
                {
                    taskCategories.Remove(task.Category);
                }
            }
            else
            {
                completedTasks.Remove(task);
                if (!completedTasks.Any(t => t.Category.CategoryName == task.Category.CategoryName))
                {
                    completedtaskCategories.Remove(task.Category);
                }
            }

        }

        private void DoneButton_OnClick(object sender, RoutedEventArgs e)
        {
            var task = (TaskModel)TasksList.SelectedItem;
            if (task.IsCompleted == false)
            {
                tasks.Remove(task);
                task.IsCompleted = true;
                task.Color = new SolidColorBrush(Colors.Red);
                completedTasks.Add(task);
                if (!tasks.Any(t => t.Category.CategoryName == task.Category.CategoryName))
                {
                    taskCategories.Remove(task.Category);
                    completedtaskCategories.Add(task.Category);
                }
                else
                {
                    completedtaskCategories.Add(task.Category);
                }
                TaskFullContent.Visibility = Visibility.Hidden;
            }
            else
            {
                completedTasks.Remove(task);
                task.IsCompleted = false;
                task.Color = new SolidColorBrush(Colors.White);
                tasks.Add(task);
                if (!tasks.Any(t => t.Category.CategoryName == task.Category.CategoryName))
                {
                    taskCategories.Add(task.Category);
                }
                else
                {
                    taskCategories.Add(task.Category);
                    completedtaskCategories.Remove(task.Category);
                }
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

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            TasksList.ItemsSource = completedTasks;
            MenuList.ItemsSource = completedtaskCategories;
            isInHistory = true;
        }

        private void TasksButton_Click(object sender, RoutedEventArgs e)
        {
            isInHistory = false;
            UpdateLists();
        }

        private void CategoriesMenuClicked(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            TaskCategory category = (TaskCategory)listBox.SelectedItem;
            try
            {
                if (!isInHistory)
                {
                    TasksList.ItemsSource = tasks.Where(t => t.Category.CategoryName == category.CategoryName);
                }
                else
                {
                    TasksList.ItemsSource = completedTasks.Where(t => t.Category.CategoryName == category.CategoryName);
                }
            }
            catch
            {

            }
        }
    }
}
