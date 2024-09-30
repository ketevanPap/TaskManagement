using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.DomainService;
using TaskManagement.Entity.Models.ApplicationClasses;
using TaskManagement.Entity.Models.Enums;
using TaskManagement.Infrastructure.DatabaseContext;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskItemRepository : Repository<TaskItem>, ITaskItemRepository
    {
        public TaskItemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByStatusAsync(TaskItemStatus status)
        {
            return await _context.TaskItems
                                 .Where(t => t.Status == status)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByPriorityAsync(TaskPriority priority)
        {
            return await _context.TaskItems
                                 .Where(t => t.Priority == priority)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByDueDateAsync(DateTime dueDate)
        {
            return await _context.TaskItems
                                 .Where(t => t.DueDate.Date == dueDate.Date)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> SearchTasksAsync(string searchTerm)
        {
            return await _context.TaskItems
                                 .Where(t => t.Title.Contains(searchTerm) || t.Description.Contains(searchTerm))
                                 .ToListAsync();
        }
        public async Task<IEnumerable<TaskItem>> GetFilteredTasksAsync(string? title, string? description, TaskItemStatus? status, TaskPriority? priority, DateTime? dueDateFrom, DateTime? dueDateTo)
        {
            var query = _context.TaskItems.AsQueryable();

            // Apply filters directly in the database query
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(t => t.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(description))
            {
                query = query.Where(t => t.Description.Contains(description));
            }

            if (status.HasValue)
            {
                query = query.Where(t => t.Status == status);
            }

            if (priority.HasValue)
            {
                query = query.Where(t => t.Priority == priority);
            }

            if (dueDateFrom.HasValue && dueDateTo.HasValue)
            {
                query = query.Where(t => t.DueDate.Date >= dueDateFrom.Value.Date && t.DueDate.Date <= dueDateTo.Value.Date);
            }
            else if (dueDateFrom.HasValue)
            {
                query = query.Where(t => t.DueDate.Date >= dueDateFrom.Value.Date);
            }
            else if (dueDateTo.HasValue)
            {
                query = query.Where(t => t.DueDate.Date <= dueDateTo.Value.Date);
            }

            return await query.ToListAsync();
        }
    }
}
