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
    }
}
