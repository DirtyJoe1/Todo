using Desktop.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Todo.Entitites;

namespace Desktop.View
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public Repository.Repository _repository;
        public ObservableCollection<TaskModel> tasks = new ObservableCollection<TaskModel>();
        public ObservableCollection<string> taskCategories = new ObservableCollection<string>();
        public ObservableCollection<TaskModel> completedTasks = new ObservableCollection<TaskModel>();
        public ObservableCollection<string> completedtaskCategories = new ObservableCollection<string>();
        public bool isInHistory = false;
        public async void SetData()
        {
            tasks = Mapper.DeMapTodo(await _repository.GetTodosAsync());
            TasksList.ItemsSource = tasks;
            foreach (var i in tasks)
            {
                if (!taskCategories.Contains(i.CategoryName))
                {
                    taskCategories.Add(i.CategoryName);
                }
                else if (taskCategories.All(cat => cat.Count() == 0)
                {

                }
            }
            MenuList.ItemsSource = taskCategories;
        }
        public async void SetUsername()
        {
            string content = await _repository.GetUser();
            UsernameLable.Content = content;
        }
        public MainPage(Repository.Repository repository)
        {
            InitializeComponent();
            _repository = repository;
            SetUsername();
            SetData();
        }
        public async void DeleteTask(TaskModel task)
        {
            var response = await _repository.DeleteTodo(task.Id);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Удалено");
            }
            else
            {
                MessageBox.Show(response.StatusCode.ToString());
            }
            SetData();
        }
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TaskCreation(this, _repository));
        }
        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var task = (TaskModel)TasksList.SelectedItem;
            if (!isInHistory)
            {
                DeleteTask(task);
            }
            else
            {
                completedTasks.Remove(task);
            }
            SetData();
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
                if (!tasks.Any(t => t.CategoryName == task.CategoryName))
                {
                    taskCategories.Remove(task.CategoryName);
                    completedtaskCategories.Add(task.CategoryName);
                }
                else
                {
                    completedtaskCategories.Add(task.CategoryName);
                }
                TaskFullContent.Visibility = Visibility.Hidden;
            }
            else
            {
                completedTasks.Remove(task);
                task.IsCompleted = false;
                task.Color = new SolidColorBrush(Colors.White);
                tasks.Add(task);
                if (!tasks.Any(t => t.CategoryName == task.CategoryName))
                {
                    taskCategories.Add(task.CategoryName);
                }
                else
                {
                    taskCategories.Add(task.CategoryName);
                    completedtaskCategories.Remove(task.CategoryName);
                }
                TaskFullContent.Visibility = Visibility.Visible;
            }
            SetData();
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
            SetData();
        }

        private void CategoriesMenuClicked(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBox listBox = (ListBox)sender;
                string category = listBox.SelectedItem.ToString();
                if (!isInHistory)
                {
                    TasksList.ItemsSource = tasks.Where(t => t.CategoryName == category);
                }
                else
                {
                    TasksList.ItemsSource = completedTasks.Where(t => t.CategoryName == category);
                }
            }
            catch
            {

            }
        }
    }
}
