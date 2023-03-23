using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Entitites;

namespace Desktop.Repository
{
    public static class UserRepository
    {
        private static readonly List<UserModel> Users = new List<UserModel>()
        {
            new UserModel{Name="Admin", Email="Admin@mail.ru", Password="1234567"},
            new UserModel{Name="User", Email="User@mail.ru", Password="7654321"}
        };
        public static UserModel GetUser(string email, string password)
        {
            return Users.FirstOrDefault(user => user.Email == email && user.Password == password);
        }
        public static bool CheckUser(string email, string password)
        {
            return Users.Contains(GetUser(email, password));
        }
        public static bool CheckEmail(string email)
        {
            return Users.All(user => user.Email != email);
        }
        public static string CurrentUserName(string email)
        {
            var user = Users.Find(_user => _user.Email == email);
            return user.Name;
        }
        public static void AddUser(string name, string email, string password)
        {
            Users.Add(new UserModel{Name = name, Email = email, Password = password});
        }
    }
}
