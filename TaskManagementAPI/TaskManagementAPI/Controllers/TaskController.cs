using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.ApplicationService.TaskServices.Commands;
using TaskManagement.Core.ApplicationService.TaskServices.Queries;
using TaskManagement.Entity.Models.Enums;

namespace TaskManagementAPI.Controllers
{
    //[Authorize]
    public class TaskController : BaseController
    {
        public TaskController(IMediator mediator, IConfiguration configuration) : base(mediator, configuration) { }
        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="command">Task creation command containing task details like title, description, due date, priority, and status.</param>
        /// <returns>Returns the created task or an error message.</returns>
        /// <remarks>
        /// **Required fields**:
        /// - `Title`: The title of the task.
        /// - `Description`: Description of the task.
        /// - `DueDate`: Date when the task is due.
        /// - `Priority`: Priority of the task (Low, Medium, High).
        /// - `Status`: Current status of the task (Open, In Progress, Completed).
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="command">Task update command containing task details like title, description, due date, priority, and status.</param>
        /// <returns>Returns the updated task or an error message.</returns>
        /// <remarks>
        /// **Required fields**:
        /// - `TaskId`: ID of the task to be updated.
        /// - Other fields as needed for updating the task.
        /// </remarks>
        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Deletes a task by ID.
        /// </summary>
        /// <param name="taskId">ID of the task to be deleted.</param>
        /// <returns>Returns success or error message.</returns>
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var result = await _mediator.Send(new DeleteTaskCommand { TaskId = taskId });
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Retrieves task details by ID.
        /// </summary>
        /// <param name="taskId">ID of the task to retrieve.</param>
        /// <returns>Returns the task details or an error message.</returns>
        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            var result = await _mediator.Send(new GetTaskByIdQuery { TaskId = taskId });
            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Retrieves all tasks with optional filtering.
        /// </summary>
        /// <param name="title">Optional task title for filtering.</param>
        /// <param name="description">Optional task description for filtering.</param>
        /// <param name="status">Optional task status for filtering.</param>
        /// <param name="priority">Optional task priority for filtering.</param>
        /// <param name="dueDateFrom">Optional start date for task filtering.</param>
        /// <param name="dueDateTo">Optional end date for task filtering.</param>
        /// <returns>Returns a list of tasks or an error message.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTasks([FromQuery] string? title, [FromQuery] string? description, [FromQuery] int? status, [FromQuery] int? priority, [FromQuery] DateTime? dueDateFrom, [FromQuery] DateTime? dueDateTo)
        {
            var query = new GetAllTasksQuery
            {
                Title = title,
                Description = description,
                Status = status.HasValue ? (TaskItemStatus?)status.Value : null,
                Priority = priority.HasValue ? (TaskPriority?)priority.Value : null,
                DueDateFrom = dueDateFrom,
                DueDateTo = dueDateTo
            };

            var result = await _mediator.Send(query);

            return result.Success ? Ok(result) : NotFound(result);
        }
    }
}
