using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.ApplicationService.UserServices.Commands;

namespace TaskManagementAPI.Controllers
{
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(IMediator mediator, IConfiguration configuration) : base(mediator, configuration) { }

        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="command">Login command containing user credentials (email and password).</param>
        /// <returns>Returns a JWT token if authentication is successful or an error message.</returns>
        /// <remarks>
        /// **Required fields**:
        /// - `Email`: The registered email of the user.
        /// - `Password`: The password for the user's account. 
        /// 
        /// **Password Format Requirements**:
        /// - Minimum 6 characters.
        /// - The password is compared to the hashed password stored in the database.
        ///
        /// **JWT Token**:
        /// - On successful login, a JWT token is returned which can be used for authorized API access.
        /// - The token should be included in the `Authorization` header as `Bearer {token}` in subsequent requests.
        /// </remarks>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
