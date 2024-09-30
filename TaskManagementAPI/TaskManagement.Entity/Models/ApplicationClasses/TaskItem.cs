using System.ComponentModel.DataAnnotations;
using TaskManagement.Entity.Models.Enums;

namespace TaskManagement.Entity.Models.ApplicationClasses
{
    public class TaskItem : BaseEntity
    {
        [Required]
        [StringLength(200, ErrorMessage = "The title cannot exceed 200 characters.")]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        [Required]
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Open;
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
