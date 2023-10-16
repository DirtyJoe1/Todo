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
                Category = model.CategoryName,
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
                var deMappedTodo = new TaskModel
                {
                    Id = Guid.Parse(todo[i].Id),
                    TaskDateTime = DateTimeOffset.FromUnixTimeSeconds(todo[i].Date).LocalDateTime,
                    Title = todo[i].Title,
                    Description = todo[i].Description,
                    IsCompleted = todo[i].IsCompleted,
                    DisplayTime = DateTimeOffset.FromUnixTimeSeconds(todo[i].Date).LocalDateTime.TimeOfDay.ToString(),
                    Color = new SolidColorBrush(Colors.White),
                    CategoryName = todo[i].Category,
                };
                deMappedTodos.Add(deMappedTodo);
            }
            return deMappedTodos;
        }
    }
}
