
namespace TaskManagement.Core.DomainService
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskItemRepository TaskItemRepository { get; }
        IUserRepository UserRepository { get; }
        Task<int> CompleteAsync();
    }
}
