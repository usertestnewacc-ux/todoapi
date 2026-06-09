using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using todoapi.Data;
using todoapi.Dtos;
using todoapi.Models;

namespace todoapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            var userId = GetUserId();
            var todos = _context.Todos.Where(t => t.UserId == userId).ToList();
            return Ok(todos);
        }

        [HttpPost]
        public IActionResult CreateTodo(CreateTodoDto dto)
        {
            var todo = new Todo
            {
                UserId = GetUserId(),
                Name = dto.Name,
                Status = "Pending"
            };

            _context.Todos.Add(todo);
            _context.SaveChanges();

            return Ok(todo);
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, UpdateTodoStatusDto dto)
        {
            var userId = GetUserId();
            var todo = _context.Todos.FirstOrDefault(t => t.Id == id && t.UserId == userId);

            if (todo == null) return NotFound();

            todo.Status = dto.Status;
            _context.SaveChanges();

            return Ok(todo);
        }
    }
}
