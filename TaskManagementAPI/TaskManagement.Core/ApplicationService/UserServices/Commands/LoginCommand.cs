using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using TaskManagement.Core.ApplicationService.Interfaces;
using TaskManagement.Core.DomainService;
using TaskManagement.Entity.Models.ApplicationClasses;
using TaskManagement.Entity.Models.DTOs;

namespace TaskManagement.Core.ApplicationService.UserServices.Commands
{
    public class LoginCommand : IRequest<ResponseModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public class Handler : IRequestHandler<LoginCommand, ResponseModel>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
            private readonly IConfiguration _configuration;
            private readonly ISecurityService _securityService;

            public Handler(IUnitOfWork unitOfWork, IPasswordHasher<ApplicationUser> passwordHasher, IConfiguration configuration, ISecurityService securityService)
            {
                _unitOfWork = unitOfWork;
                _passwordHasher = passwordHasher;
                _configuration = configuration;
                _securityService = securityService;
            }

            public async Task<ResponseModel> Handle(LoginCommand command, CancellationToken cancellationToken)
            {
                var user = await _unitOfWork.UserRepository.GetUserByEmailAsync(command.Email);

                if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, command.Password) != PasswordVerificationResult.Success)
                {
                    return new ResponseModel
                    {
                        Success = false,
                        Message = "Invalid email or password"
                    };
                }

                // Create JWT Token using the SecurityService
                var token = _securityService.GenerateJwtToken(user);

                return new ResponseModel
                {
                    Success = true,
                    Data = new
                    {
                        token = token,
                        user = user.Adapt<UserDTO>(),
                    }
                };
            }
        }
    }

}
