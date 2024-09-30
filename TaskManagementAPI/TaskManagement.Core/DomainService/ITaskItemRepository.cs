
using TaskManagement.Entity.Models.ApplicationClasses;
using TaskManagement.Entity.Models.Enums;

namespace TaskManagement.Core.DomainService
{
    public interface ITaskItemRepository : IRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetTasksByStatusAsync(TaskItemStatus status);
        Task<IEnumerable<TaskItem>> GetTasksByPriorityAsync(TaskPriority priority);
        Task<IEnumerable<TaskItem>> GetTasksByDueDateAsync(DateTime dueDate);
        Task<IEnumerable<TaskItem>> SearchTasksAsync(string searchTerm);
        Task<IEnumerable<TaskItem>> GetFilteredTasksAsync(string? title, string? description, TaskItemStatus? status, TaskPriority? priority, DateTime? dueDateFrom, DateTime? dueDateTo);
    }
}
