using MediatR;
using TaskManagement.Core.DomainService;
using TaskManagement.Entity.Models.DTOs;
using TaskManagement.Entity.Models.Enums;

namespace TaskManagement.Core.ApplicationService.TaskServices.Queries
{
    public class GetAllTasksQuery : IRequest<ResponseModel>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TaskItemStatus? Status { get; set; }
        public TaskPriority? Priority { get; set; }
        public DateTime? DueDateFrom { get; set; }
        public DateTime? DueDateTo { get; set; }
    }

    public class Handler : IRequestHandler<GetAllTasksQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var tasks = await _unitOfWork.TaskItemRepository.GetFilteredTasksAsync(
                    request.Title, request.Description, request.Status, request.Priority, request.DueDateFrom, request.DueDateTo);

                return new ResponseModel
                {
                    Success = true,
                    Message = "Tasks retrieved successfully",
                    Data = tasks
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = $"Error occurred while retrieving tasks: {ex.Message}"
                };
            }
        }
    }
}
