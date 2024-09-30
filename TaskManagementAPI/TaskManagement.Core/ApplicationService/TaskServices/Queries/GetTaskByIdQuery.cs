using MediatR;
using TaskManagement.Core.DomainService;
using TaskManagement.Entity.Models.DTOs;

namespace TaskManagement.Core.ApplicationService.TaskServices.Queries
{
    public class GetTaskByIdQuery : IRequest<ResponseModel>
    {
        public int TaskId { get; set; }

        public class Handler : IRequestHandler<GetTaskByIdQuery, ResponseModel>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ResponseModel> Handle(GetTaskByIdQuery query, CancellationToken cancellationToken)
            {
                try
                {
                    var task = await _unitOfWork.TaskItemRepository.GetByIdAsync(query.TaskId);

                    if (task == null)
                    {
                        return new ResponseModel
                        {
                            Success = false,
                            Message = "Task not found"
                        };
                    }

                    return new ResponseModel
                    {
                        Success = true,
                        Data = task
                    };
                }
                catch (Exception ex)
                {
                    return new ResponseModel
                    {
                        Success = false,
                        Message = $"Error occurred while retrieving task: {ex.Message}"
                    };
                }
            }
        }
    }
}
