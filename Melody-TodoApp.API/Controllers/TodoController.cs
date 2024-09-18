using Melody.TodoApp.Domain.Entities;
using Melody_TodoApp.Application.Interfaces;
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
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
           _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodo()
        {
           var todos = await _todoService.GetAllTodoAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
          var todo =  await _todoService.GetTodoById(id);
          if(todo == null)
            {
                return BadRequest("todo not found");
            }
          return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo([FromBody] Todo todo)
        {
            var todos = await _todoService.AddTodoAsync(todo);
            
            return Ok(await _todoService.GetAllTodoAsync());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTodoAsync(Todo updatedTodo)
        {
           await _todoService.UpdateTodoAsync(updatedTodo);
           return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
           await _todoService.DeleteTodoAsync(id);
            return NoContent();
           
        }
    }
}
