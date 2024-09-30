using MediatR;
using TaskManagement.Core.DomainService;
using TaskManagement.Entity.Models.ApplicationClasses;
using TaskManagement.Entity.Models.DTOs;
using TaskManagement.Entity.Models.Enums;

namespace TaskManagement.Core.ApplicationService.TaskServices.Commands
{
    public class CreateTaskCommand : IRequest<ResponseModel>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskItemStatus Status { get; set; }
        public string? AssignedUserId { get; set; }

        public class Handler : IRequestHandler<CreateTaskCommand, ResponseModel>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ResponseModel> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var task = new TaskItem
                    {
                        Title = command.Title,
                        Description = command.Description,
                        DueDate = command.DueDate,
                        Priority = command.Priority,
                        Status = command.Status,
                        UserId = command.AssignedUserId,
                        CreatedBy = "451b02a5-d72a-4872-bd26-1ee859fa8a65"
                    };

                    await _unitOfWork.TaskItemRepository.AddAsync(task);
                    await _unitOfWork.CompleteAsync();

                    return new ResponseModel
                    {
                        Success = true,
                        Message = "Task created successfully",
                        Data = task
                    };
                }
                catch (Exception ex)
                {
                    return new ResponseModel
                    {
                        Success = false,
                        Message = $"Error occurred while creating task: {ex.Message}"
                    };
                }
            }
        }
    }
}

