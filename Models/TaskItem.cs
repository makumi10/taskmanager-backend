using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    [Table("tasks")]
    public class TaskItem
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Required]
        [Column("status")]
        public string Status { get; set; } = "pending"; // pending | in_progress | completed

        [Column("create_date")]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [Column("due_date")]
        public DateTime? DueDate { get; set; }
    }
}
