
using System.Runtime.Serialization;

namespace TaskManagement.Entity.Models.Enums
{
    public enum TaskPriority
    {
        [EnumMember(Value = "Low")]
        Low = 0,

        [EnumMember(Value = "Medium")]
        Medium = 1,

        [EnumMember(Value = "High")]
        High = 2
    }
}
