using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        // Gets all tasks
        public async Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync()
        {
            var tasks = await _context.Tasks
                .OrderByDescending(t => t.CreateDate)
                .ToListAsync();

            return tasks.Select(MapToResponseDto);
        }

        // Gets single task by ID
        public async Task<TaskResponseDto?> GetTaskByIdAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task == null ? null : MapToResponseDto(task);
        }

        // Creates a new task
        public async Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                DueDate = dto.DueDate,
                CreateDate = DateTime.UtcNow
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return MapToResponseDto(task);
        }

        // Updates an existing task
        public async Task<TaskResponseDto?> UpdateTaskAsync(Guid id, UpdateTaskDto dto)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return null;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.DueDate = dto.DueDate;

            await _context.SaveChangesAsync();

            return MapToResponseDto(task);
        }

        // Deletes a task
        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return true;
        }

        // Private helper: maps TaskItem to TaskResponseDto
        private static TaskResponseDto MapToResponseDto(TaskItem task)
        {
            return new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                CreateDate = task.CreateDate,
                DueDate = task.DueDate
            };
        }
    }
}
