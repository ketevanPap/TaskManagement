
namespace TaskManagement.Entity.Models.ApplicationClasses
{
    public class TaskAssignment // use it if we need a many-to-many connection between User and Task
    {
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int TaskItemId { get; set; }
        public TaskItem? TaskItem { get; set; }
        public string? AssignedById { get; set; }
        public ApplicationUser? AssignedBy { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
