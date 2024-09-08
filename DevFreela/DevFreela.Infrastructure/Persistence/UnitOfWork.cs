using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevFreela.Infrastructure.Persistence;

public class UnitOfWork(DevFreelaDbContext dbContext, IProjectRepository projects, IUserRepository users, ISkillRepository skills) : IUnitOfWork {
    private readonly DevFreelaDbContext _dbContext = dbContext;
    private IDbContextTransaction _transaction;

    public IProjectRepository Projects { get; } = projects;
    public IUserRepository Users { get; } = users;
    public ISkillRepository Skills { get; } = skills;

    public Task<int> CompleteAsync() => this._dbContext.SaveChangesAsync();

    public async Task BeginTransactionAsync() => this._transaction = await this._dbContext.Database.BeginTransactionAsync();

    public async Task CommitAsync() {
        try {
            await this._transaction.CommitAsync();
        } catch (Exception) {
            await this._transaction.RollbackAsync();
            throw;
        }
    }

    public void Dispose() {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
        if (disposing)
            this._dbContext.Dispose();
    }
}