using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Entitites;

namespace Desktop.Repository
{
    public static class UserRepository
    {
        private static List<UserModel> users = new List<UserModel>()
        {
            new UserModel{Name="Admin", Email="Admin@mail.ru", Password="1234567"},
            new UserModel{Name="User", Email="User@mail.ru", Password="7654321"}
        };
        public static UserModel GetUser(string email, string password)
        {
            return users.FirstOrDefault(user => user.Email == email && user.Password == password);
        }
        public static bool CheckUser(string email, string password)
        {
            return users.Contains(GetUser(email, password));
        }
        public static bool CheckEmail(string email)
        {
            bool isUnique = true;
            foreach (UserModel user in users)
            {
                if (user.Email == email)
                {
                    isUnique = false;
                    break;
                }
            }
            return isUnique;
        }
        public static void AddUser(string name, string email, string password)
        {
            users.Add(new UserModel
            {
                Name = name,
                Email = email,
                Password = password
            });
        }
    }
}
