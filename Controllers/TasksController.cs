using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>Creates a new task.</summary>
        /// <param name="dto">Task details</param>
        /// <returns>The newly created task</returns>
        /// <response code="201">Task created successfully</response>
        /// <response code="400">Validation failed</response>
        
        [HttpPost]
        [ProducesResponseType(typeof(TaskResponseDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<TaskResponseDto>> CreateTask([FromBody] CreateTaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = await _taskService.CreateTaskAsync(dto);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        /// <summary>Retrieves all tasks ordered by creation date.</summary>
        /// <returns>List of all tasks</returns>
        /// <response code="200">Returns the list of tasks</response>

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskResponseDto>), 200)]
        public async Task<ActionResult<IEnumerable<TaskResponseDto>>> GetTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        /// <summary>Retrieves a single task by ID.</summary>
        /// <param name="id">The task GUID</param>
        /// <returns>The requested task</returns>
        /// <response code="200">Task found</response>
        /// <response code="404">Task not found</response>

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskResponseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TaskResponseDto>> GetTask(Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if (task == null)
                return NotFound(new { message = $"Task with ID {id} was not found." });

            return Ok(task);
        }

        /// <summary>Updates an existing task.</summary>
        /// <param name="id">The task GUID</param>
        /// <param name="dto">Updated task details</param>
        /// <returns>The updated task</returns>
        /// <response code="200">Task updated successfully</response>
        /// <response code="400">Validation failed</response>
        /// <response code="404">Task not found</response>

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TaskResponseDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TaskResponseDto>> UpdateTask(Guid id, [FromBody] UpdateTaskDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = await _taskService.UpdateTaskAsync(id, dto);

            if (task == null)
                return NotFound(new { message = $"Task with ID {id} was not found." });

            return Ok(task);
        }

        /// <summary>Deletes a task by ID.</summary>
        /// <param name="id">The task GUID</param>
        /// <response code="204">Task deleted successfully</response>
        /// <response code="404">Task not found</response>

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);

            if (!deleted)
                return NotFound(new { message = $"Task with ID {id} was not found." });

            return NoContent();
        }
    }
}
