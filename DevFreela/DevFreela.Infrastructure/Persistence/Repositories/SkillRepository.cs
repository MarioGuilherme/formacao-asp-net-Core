using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class SkillRepository(DevFreelaDbContext dbContext) : ISkillRepository {
    private readonly DevFreelaDbContext _dbContext = dbContext;

    public Task<List<Skill>> GetAllAsync() => this._dbContext.Skills.ToListAsync();

    public async Task AddSkillFromProject(Project project) {
        string[] words = project.Description.Split(' ');
        string skill = $"{project.Id} - {words[^1]}";

        await this._dbContext.Skills.AddAsync(new(skill));
    }
}