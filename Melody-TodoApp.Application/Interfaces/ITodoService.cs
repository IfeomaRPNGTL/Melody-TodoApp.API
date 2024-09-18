using Melody.TodoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody_TodoApp.Application.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetAllTodoAsync();
        Task<Todo> GetTodoById(int id);
        Task<List<Todo>> AddTodoAsync(Todo todo);
        Task<List<Todo>> UpdateTodoAsync(Todo updatedTodo);
        Task<List<Todo>> DeleteTodoAsync(int id);
    }
}
