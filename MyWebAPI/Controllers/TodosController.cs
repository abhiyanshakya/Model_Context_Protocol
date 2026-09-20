using Microsoft.AspNetCore.Mvc;
using Api.Services;
using Api.Models;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TodosController(ITodoService service) : ControllerBase
{
    public readonly ITodoService _service = service; 
    [HttpGet]
    [ProducesResponseType(typeof(List<TodoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    
    public async Task<IActionResult>GetTodos()
    {
        try
        {
            var todos = await _service.GetTodosAsync();
            return Ok(todos);
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.Status, new ErrorResponse
            {
                Status = ex.Status,
                Error = ex.Error,
                Message = ex.Message
            });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(List<TodoDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult>PostTodo([FromBody] TodoDto todo)
    {
        try
        {
            var createdTodo = await _service.PostTodoAsync(todo);
            return StatusCode(StatusCodes.Status201Created, createdTodo);
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.Status, new ErrorResponse
            {
                Status = ex.Status,
                Error = ex.Error,
                Message = ex.Message
            });
        }
    }
}