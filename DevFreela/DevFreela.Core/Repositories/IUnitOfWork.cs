using DevFreela.Core.Repositories;

namespace DevFreela.Core.Persistence;

public interface IUnitOfWork : IDisposable {
    IProjectRepository Projects { get; }
    IUserRepository Users { get; }
    ISkillRepository Skills { get; }
    Task<int> CompleteAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
}