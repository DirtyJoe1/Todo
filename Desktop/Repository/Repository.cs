using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Todo.Entitites;
using Todo.Entitites.DataTransferObjects;

namespace Desktop.Repository
{
    public class Repository : HttpClientDesktop
    {
        private readonly string LoginUrl = "api/auth/login";
        private readonly string RegistrationUrl = "api/auth/registration";
        private readonly string TodosUrl = "api/todos";
        private readonly string MarkUrl = "api/todos/mark";
        private readonly string FileUrl = "api/user/photo/";
        private readonly string UserUrl = "api/user";
        private readonly HttpClient _httpClient;
        private readonly string connectionString = "Server=79.174.94.77;Port=3306;Database=TodoApp;Uid=admin;Pwd=123qweasdzxc;";
        private static string Token;
        public Repository()
        {
            _httpClient = GetHttpClient();
            Token = GetToken();
            if (!string.IsNullOrEmpty(Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }
        }
        public string GetToken()
        {
            string token = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Token FROM Tokens";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("Token")))
                        {
                            token = reader.GetString("Token");
                        }
                    }
                }
                connection.Close();
            }
            return token;
        }
        public bool ValidateToken()
        {
            if(Token != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void InsertToken(string token)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Tokens (token) VALUES (@token) ON DUPLICATE KEY UPDATE token = @token";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@token", token);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void DeleteToken()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Tokens";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public async Task<HttpResponseMessage> PostUserLoginAsync(LoginModelDto login)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(LoginUrl, login);
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(await responseMessage.Content.ReadAsStringAsync());
            Token = responseData.data.access_token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            InsertToken(Token);
            return responseMessage;
        }
        public async Task<HttpResponseMessage> PostUserRegistrationAsync(RegistrationModelDto register)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(RegistrationUrl, register);
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(await responseMessage.Content.ReadAsStringAsync());
            Token = responseData.access_token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            InsertToken(Token);
            return responseMessage;
        }
        public async Task<string> GetPhoto(string id)
        {
            return await _httpClient.GetFromJsonAsync<string>(FileUrl + id);
        }
        public async Task<List<TodoDto>> GetTodosAsync()
        {
            var responseMessage = await _httpClient.GetStringAsync(TodosUrl);
            var todoList = Newtonsoft.Json.JsonConvert.DeserializeObject<TodoListDto>(responseMessage);
            return todoList.Data;
        }
        public async Task<HttpResponseMessage> PostTodoAsync(TaskModel task)
        {
            TodoDto todo = Mapper.MapTodo(task);
            var data = await _httpClient.PostAsJsonAsync(TodosUrl, todo);
            return data;
        }
        public async Task<HttpResponseMessage> DeleteTodo(Guid id)
        {
            return await _httpClient.DeleteAsync(TodosUrl + "/" + id.ToString());
        }
        public async Task<HttpResponseMessage> PutMark(Guid id, bool IsCompleted)
        {
            var putData = new StringContent(IsCompleted.ToString());
            return await _httpClient.PutAsync(MarkUrl + id.ToString(), putData);
        }
        public async Task<string> GetUser()
        {
            string message = await _httpClient.GetStringAsync(UserUrl);
            var userData = Newtonsoft.Json.JsonConvert.DeserializeObject<UserModel>(message);
            return userData.Name;
        }
        public async Task<HttpResponseMessage> PostPhoto(string uploadedFile)
        {
            return await _httpClient.PostAsJsonAsync(FileUrl, uploadedFile);
        }
    }
}
