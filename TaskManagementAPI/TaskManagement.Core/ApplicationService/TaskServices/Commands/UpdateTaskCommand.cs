
using MediatR;
using TaskManagement.Core.DomainService;
using TaskManagement.Entity.Models.DTOs;
using TaskManagement.Entity.Models.Enums;

namespace TaskManagement.Core.ApplicationService.TaskServices.Commands
{
    public class UpdateTaskCommand : IRequest<ResponseModel>
    {
        public int TaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskItemStatus Status { get; set; }
        public string? AssignedUserId { get; set; }

        public class Handler : IRequestHandler<UpdateTaskCommand, ResponseModel>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ResponseModel> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var task = await _unitOfWork.TaskItemRepository.GetByIdAsync(command.TaskId);

                    if (task == null)
                    {
                        return new ResponseModel
                        {
                            Success = false,
                            Message = "Task not found"
                        };
                    }

                    task.Title = command.Title;
                    task.Description = command.Description;
                    task.DueDate = command.DueDate;
                    task.Priority = command.Priority;
                    task.Status = command.Status;
                    task.UserId = command.AssignedUserId;
                    task.UpdatedBy = "451b02a5-d72a-4872-bd26-1ee859fa8a65";
                    task.UpdatedAt = DateTime.UtcNow;

                    await _unitOfWork.CompleteAsync();

                    return new ResponseModel
                    {
                        Success = true,
                        Message = "Task updated successfully",
                        Data = task
                    };
                }
                catch (Exception ex)
                {
                    return new ResponseModel
                    {
                        Success = false,
                        Message = $"Error occurred while updating task: {ex.Message}"
                    };
                }
            }
        }
    }
}
