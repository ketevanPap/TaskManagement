using Mapster;
using MediatR;
using TaskManagement.Core.DomainService;
using TaskManagement.Entity.Models.DTOs;

namespace TaskManagement.Core.ApplicationService.UserServices.Queries
{
    public class GetAllUsersQuery : IRequest<ResponseModel>
    {
    }

    public class Handler : IRequestHandler<GetAllUsersQuery, ResponseModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseModel> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _unitOfWork.UserRepository.GetAllUsersAsync();
                // Map users to UserDTO
                var userDtos = users.Adapt<IEnumerable<UserDTO>>();

                return new ResponseModel
                {
                    Success = true,
                    Message = "Users retrieved successfully",
                    Data = userDtos
                };
            }
            catch (Exception ex)
            {
                return new ResponseModel
                {
                    Success = false,
                    Message = $"Error occurred while retrieving users: {ex.Message}"
                };
            }
        }
    }
}
