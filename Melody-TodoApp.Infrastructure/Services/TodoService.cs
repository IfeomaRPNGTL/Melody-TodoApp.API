using Melody.TodoApp.Domain.Entities;
using Melody_TodoApp.Application.Interfaces;
using Melody_TodoApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melody_TodoApp.Infrastructure.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoDbContext _todoDbContext;
        public TodoService(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<List<Todo>> AddTodoAsync(Todo todo)
        {
            _todoDbContext.Todos.Add(todo);
            await _todoDbContext.SaveChangesAsync();
            return await _todoDbContext.Todos.ToListAsync();
        }

        public async Task<List<Todo>> DeleteTodoAsync(int id)
        {
            var deleteTodo = await _todoDbContext.Todos.FindAsync(id);
            if (deleteTodo is null)
            {
                Console.WriteLine("Todo not found");
            }
            _todoDbContext.Todos.Remove(deleteTodo);
            await _todoDbContext.SaveChangesAsync();
            return await _todoDbContext.Todos.ToListAsync();
        }

        public async Task<IEnumerable<Todo>> GetAllTodoAsync()
        {
            var todos = await _todoDbContext.Todos.ToListAsync();
            return todos;
        }

        public async Task<Todo> GetTodoById(int id)
        {
            var todo = await _todoDbContext.Todos.FindAsync(id);
            if (todo == null)
            {
                Console.WriteLine("todo not found");
            }
            return todo;
        }

        public async Task<List<Todo>> UpdateTodoAsync(Todo updatedTodo)
        {
            var dbTodo = await _todoDbContext.Todos.FindAsync(updatedTodo.Id);
            if (dbTodo is null)
            {
                Console.WriteLine("Todo not found");
            }
            dbTodo.Name = updatedTodo.Name;
            dbTodo.Description = updatedTodo.Description;
            dbTodo.Duedate = updatedTodo.Duedate;
            return await _todoDbContext.Todos.ToListAsync();

        }
    }
}
