using MediatR;
using TaskManagement.Core.DomainService;
using TaskManagement.Entity.Models.DTOs;

namespace TaskManagement.Core.ApplicationService.TaskServices.Commands
{
    public class DeleteTaskCommand : IRequest<ResponseModel>
    {
        public int TaskId { get; set; }

        public class Handler : IRequestHandler<DeleteTaskCommand, ResponseModel>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ResponseModel> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
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

                    _unitOfWork.TaskItemRepository.Remove(task);
                    await _unitOfWork.CompleteAsync();

                    return new ResponseModel
                    {
                        Success = true,
                        Message = "Task deleted successfully"
                    };
                }
                catch (Exception ex)
                {
                    return new ResponseModel
                    {
                        Success = false,
                        Message = $"Error occurred while deleting task: {ex.Message}"
                    };
                }
            }
        }
    }
}
