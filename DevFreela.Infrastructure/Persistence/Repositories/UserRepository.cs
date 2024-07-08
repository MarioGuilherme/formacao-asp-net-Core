using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository {
    private readonly DevFreelaDbContext _dbContext;

    public UserRepository(DevFreelaDbContext dbContext) {
        this._dbContext = dbContext;
    }

    public async Task<User> GetByIdAsync(int id) {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<int> CreateUserAsync(User user) {
        await this._dbContext.Users.AddAsync(user);
        await this._dbContext.SaveChangesAsync();

        return user.Id;
    }
}