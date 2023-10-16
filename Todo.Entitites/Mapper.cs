using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Todo.Entitites.DataTransferObjects;

namespace Todo.Entitites
{
    public class Mapper
    {
        public static TodoDto MapTodo(TaskModel model)
        {
            return new TodoDto
            {
                Id = model.Id.ToString(),
                Category = model.Category.CategoryName,
                Title = model.Title,
                Description = model.Description,
                Date = (int)model.TaskDateTime.ToBinary(),
                IsCompleted = model.IsCompleted,
            };
        }
        public static LoginModelDto MapLogin(UserModel user)
        {
            return new LoginModelDto
            {
                Email = user.Email,
                Password = user.Password,
            };
        }
        public static RegistrationModelDto MapRegistration(UserModel user)
        {
            return new RegistrationModelDto
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
        }
        public static ObservableCollection<TaskModel> DeMapTodo(List<TodoDto> todo)
        {
            var deMappedTodos = new ObservableCollection<TaskModel>();
            for (int i = 0; i < todo.Count(); i++)
            {
                Random random = new Random();
                var deMappedTodo = new TaskModel
                {
                    TaskDateTime = DateTimeOffset.FromUnixTimeSeconds(todo[i].Date).LocalDateTime,
                    Title = todo[i].Title,
                    Description = todo[i].Description,
                    IsCompleted = todo[i].IsCompleted,
                    VisibleTime = DateTimeOffset.FromUnixTimeSeconds(todo[i].Date).LocalDateTime.Hour.ToString(),
                    Color = new SolidColorBrush(Colors.White),
                    Category = new TaskCategory
                    {
                        CategoryName = todo[i].Category,
                        Color = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)))
                    }
                };
                deMappedTodos.Add(deMappedTodo);
            }
            return deMappedTodos;
        }
    }
}
