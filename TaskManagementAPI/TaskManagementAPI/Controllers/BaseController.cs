using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly IConfiguration _configuration;

        public BaseController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        // Method to get the currently authorized user's data
        protected string GetUserId()
        {
            return User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        protected string GetUserName()
        {
            return User?.Identity?.Name;
        }

        protected string GetUserEmail()
        {
            return User?.FindFirstValue(ClaimTypes.Email);
        }
    }
}
