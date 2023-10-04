using Application;

namespace MellonThinClient.Data
{
    public interface IToDosRepository
    {
        Task<List<ToDo>> GetAsync();
        Task<ToDo?> CreateAsync(ToDo toDo);
        Task<ToDo?> GetByIdAsync(int id);
        Task<ToDo> UpdateAsync(ToDo toDo);
        Task DeleteAsync(int id);
    }
}