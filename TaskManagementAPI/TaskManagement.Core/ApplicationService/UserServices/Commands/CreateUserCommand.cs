using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Core.DomainService;
using TaskManagement.Entity.Models.ApplicationClasses;
using TaskManagement.Entity.Models.DTOs;

namespace TaskManagement.Core.ApplicationService.UserServices.Commands
{
    public class CreateUserCommand : IRequest<ResponseModel>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public CreateUserCommand(string userName, string email, string password)
        {
            UserName = userName;
            Email = email;
            Password = password;
        }

        public class Handler : IRequestHandler<CreateUserCommand, ResponseModel>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

            public Handler(IUnitOfWork unitOfWork, IPasswordHasher<ApplicationUser> passwordHasher)
            {
                _unitOfWork = unitOfWork;
                _passwordHasher = passwordHasher;
            }

            public async Task<ResponseModel> Handle(CreateUserCommand command, CancellationToken cancellationToken)
            {
                var user = new ApplicationUser
                {
                    UserName = command.UserName,
                    Email = command.Email
                };

                // Hash the password
                user.PasswordHash = _passwordHasher.HashPassword(user, command.Password);

                try
                {
                    await _unitOfWork.UserRepository.AddAsync(user);
                    await _unitOfWork.CompleteAsync();

                    return new ResponseModel
                    {
                        Success = true,
                        Message = "User created successfully",
                        Data = user
                    };
                }
                catch (Exception ex)
                {
                    return new ResponseModel
                    {
                        Success = false,
                        Message = $"Error occurred: {ex.Message}"
                    };
                }
            }
        }
    }
}
