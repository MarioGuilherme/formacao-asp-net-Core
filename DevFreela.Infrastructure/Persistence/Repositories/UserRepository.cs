using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class UserRepository(DevFreelaDbContext dbContext) : IUserRepository {
    private readonly DevFreelaDbContext _dbContext = dbContext;

    public async Task<User> GetByIdAsync(int id) => await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

    public async Task<int> CreateUserAsync(User user) {
        await this._dbContext.Users.AddAsync(user);
        await this._dbContext.SaveChangesAsync();

        return user.Id;
    }

    public async Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash) => await this._dbContext.Users.SingleOrDefaultAsync(u => u.Email == email && u.Password == passwordHash);
}