using Application;

namespace MellonThinClient.Services.ToDosApiService
{
    public interface IToDosApiClient
    {
        Task<bool> DeleteToDoAsync(int id);
        Task<ToDo?> GetToDoAsync(int id);
        Task<List<ToDo>> GetToDosAsync();
        Task<ToDo?> PostToDoAsync(ToDo toDo);
        Task<bool> PutToDoAsync(int id, ToDo toDo);
    }
}