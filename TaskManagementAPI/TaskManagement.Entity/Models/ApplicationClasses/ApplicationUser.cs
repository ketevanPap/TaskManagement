

using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Entity.Models.ApplicationClasses
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<TaskItem>? TaskItems { get; set; }
    }
}
