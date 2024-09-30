
using Mapster;
using MediatR;
using TaskManagement.Core.DomainService;
using TaskManagement.Entity.Models.DTOs;

namespace TaskManagement.Core.ApplicationService.UserServices.Queries
{
    public class GetUserQuery : IRequest<ResponseModel>
    {
        public string UserId { get; set; }

        public GetUserQuery(string userId)
        {
            UserId = userId;
        }

        public class Handler : IRequestHandler<GetUserQuery, ResponseModel>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<ResponseModel> Handle(GetUserQuery query, CancellationToken cancellationToken)
            {
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(query.UserId);

                if (user == null)
                {
                    return new ResponseModel
                    {
                        Success = false,
                        Message = "User not found"
                    };
                }

                return new ResponseModel
                {
                    Success = true,
                    Data = user.Adapt<UserDTO>(),
                };
            }
        }
    }
}
