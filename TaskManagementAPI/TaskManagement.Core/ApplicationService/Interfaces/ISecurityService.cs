using TaskManagement.Entity.Models.ApplicationClasses;

namespace TaskManagement.Core.ApplicationService.Interfaces
{
    public interface ISecurityService
    {
        string GenerateJwtToken(ApplicationUser user);
    }
}
