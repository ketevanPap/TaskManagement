using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.ApplicationService.UserServices.Commands;
using TaskManagement.Core.ApplicationService.UserServices.Queries;
using TaskManagement.Entity.Models.DTOs;

namespace TaskManagementAPI.Controllers
{
    //[Authorize]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator, IConfiguration configuration) : base(mediator ,configuration) { }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>Returns user details or a not found message.</returns>
        /// <remarks>
        /// **Required fields**:
        /// - `userId`: The unique ID of the user to retrieve.
        /// </remarks>
        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUser(string userId)
        {
            var result = await _mediator.Send(new GetUserQuery(userId));

            if (result.Success)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="command">User creation command containing user details such as username, email, and password.</param>
        /// <returns>Returns the created user or an error message.</returns>
        /// <remarks>
        /// **Required fields**:
        /// - `UserName`: The username of the new user.
        /// - `Email`: The email of the new user.
        /// - `Password`: The password of the new user. 
        /// 
        /// **Password Format Requirements**:
        /// - Minimum 6 characters.
        /// - Password should not be stored as plain text. It will be hashed using the Identity framework.
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            if (command == null)
            {
                return BadRequest(new ResponseModel
                {
                    Success = false,
                    Message = "Invalid user data."
                });
            }

            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Retrieves all users in the system.
        /// </summary>
        /// <returns>Returns a list of all users or a not found message.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());

            if (result.Success)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

    }
}
