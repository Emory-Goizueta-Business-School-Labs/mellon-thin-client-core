using System;
using Application;
using MellonThinClient.Services.ToDosApiService;

namespace MellonThinClient.Data
{
    public class ToDosRepository : IToDosRepository
    {
        readonly IToDosApiClient _client;

        public ToDosRepository(IToDosApiClient toDosApiClient)
        {
            this._client = toDosApiClient;
        }

        public Task<List<ToDo>> GetAsync()
        {
            return _client.GetToDosAsync();
        }

        public Task<ToDo?> CreateAsync(ToDo toDo)
        {
            return _client.PostToDoAsync(toDo);
        }

        public Task<ToDo?> GetByIdAsync(int id)
        {
            return _client.GetToDoAsync(id);
        }

        public async Task<ToDo> UpdateAsync(ToDo toDo)
        {
            bool result = await _client.PutToDoAsync(toDo.Id, toDo);

            if (result)
            {
                return toDo;
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _client.DeleteToDoAsync(id);
        }
    }
}

