using System.ComponentModel.DataAnnotations;
using TaskManager.Validators;

namespace TaskManager.DTOs
{
    // DTO for creating a new task
    public class CreateTaskDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(255, ErrorMessage = "Title cannot exceed 255 characters")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [RegularExpression("pending|in_progress|completed",
            ErrorMessage = "Status must be pending, in_progress, or completed")]
        public string Status { get; set; } = "pending";

        [FutureDate]
        public DateTime? DueDate { get; set; }
    }

    // DTO for updating an existing task
    public class UpdateTaskDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(255, ErrorMessage = "Title cannot exceed 255 characters")]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [RegularExpression("pending|in_progress|completed",
            ErrorMessage = "Status must be pending, in_progress, or completed")]
        public string Status { get; set; } = string.Empty;
        
        [FutureDate]
        public DateTime? DueDate { get; set; }
    }

    // DTO for returning task details in responses
    public class TaskResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
