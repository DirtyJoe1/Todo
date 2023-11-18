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
        private static string Token;
        public Repository()
        {
            _httpClient = GetHttpClient();
        }
        public async Task<HttpResponseMessage> PostUserLoginAsync(LoginModelDto login)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(LoginUrl, login);
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(await responseMessage.Content.ReadAsStringAsync());
            Token = responseData.data.access_token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            return responseMessage;
        }
        public async Task<HttpResponseMessage> PostUserRegistrationAsync(RegistrationModelDto register)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(RegistrationUrl, register);
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(await responseMessage.Content.ReadAsStringAsync());
            Token = responseData.access_token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
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
