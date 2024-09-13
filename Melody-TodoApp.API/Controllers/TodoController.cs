using Melody.TodoApp.Domain.Entities;
using Melody_TodoApp.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Melody_TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _todoContext;

        public TodoController(TodoDbContext todoContext)
        {
            _todoContext = todoContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodo()
        {
           var todos = await _todoContext.Todos.ToListAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
          var todo =  await _todoContext.Todos.FindAsync(id);
          if(todo == null)
            {
                return BadRequest("todo not found");
            }
          return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodoAsync(Todo todo)
        {
            _todoContext.Todos.Add(todo);
            await _todoContext.SaveChangesAsync();
            return Ok(await _todoContext.Todos.ToListAsync());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodoAsync(Todo updatedTodo)
        {
           var dbTodo = await _todoContext.Todos.FindAsync(updatedTodo.Id);
            if(dbTodo is null)
            {
                return NotFound("Todo not found");
            }
            dbTodo.Name = updatedTodo.Name;
            dbTodo.Description = updatedTodo.Description;
            dbTodo.Duedate = updatedTodo.Duedate;
            return Ok(await _todoContext.Todos.ToListAsync());

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTodo(int id)
        {
           var deleteTodo = await _todoContext.Todos.FindAsync(id);
            if(deleteTodo is null)
            {
                return NotFound("Todo not found");
            }
            _todoContext.Todos.Remove(deleteTodo);
            await _todoContext.SaveChangesAsync();
            return Ok(await _todoContext.Todos.ToListAsync());
        }
    }
}
