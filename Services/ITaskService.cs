using TaskManager.DTOs;

namespace TaskManager.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskResponseDto>> GetAllTasksAsync();
        Task<TaskResponseDto?> GetTaskByIdAsync(Guid id);
        Task<TaskResponseDto> CreateTaskAsync(CreateTaskDto dto);
        Task<TaskResponseDto?> UpdateTaskAsync(Guid id, UpdateTaskDto dto);
        Task<bool> DeleteTaskAsync(Guid id);
    }
}
