
using System.Runtime.Serialization;

namespace TaskManagement.Entity.Models.Enums
{
    public enum TaskItemStatus
    {
        [EnumMember(Value = "Open")]
        Open = 0,

        [EnumMember(Value = "In Progress")]
        InProgress = 1,

        [EnumMember(Value = "Completed")]
        Completed = 2
    }
}
