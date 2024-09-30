
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Entity.Models.ApplicationClasses
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [Required]
        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
