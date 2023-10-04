using System;
using Application;
using System.Linq;

namespace MellonThinClient.Services.ToDosApiService
{
    public class ToDosApiClient : IToDosApiClient
    {
        private readonly HttpClient _httpClient;

        public ToDosApiClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<List<ToDo>> GetToDosAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<ToDo>>("ToDos");

            if (response is null)
            {
                return new List<ToDo>();
            }

            return response;
        }

        public async Task<ToDo?> PostToDoAsync(ToDo toDo)
        {
            var response = await _httpClient.PostAsJsonAsync<ToDo>("ToDos", toDo);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ToDo>();
        }

        public Task<ToDo?> GetToDoAsync(int id)
        {
            return _httpClient.GetFromJsonAsync<ToDo>($"ToDos/{ id }");
        }

        public async Task<bool> PutToDoAsync(int id, ToDo toDo)
        {
            var response = await _httpClient.PutAsJsonAsync($"ToDos/{ id }", toDo);

            response.EnsureSuccessStatusCode();

            return true;
        }

        public async Task<bool> DeleteToDoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"ToDos/{id}");

            response.EnsureSuccessStatusCode();

            return true;
        }
    }
}

