using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations;

public class SkillService : ISkillService {
    private readonly DevFreelaDbContext _dbContext;

    public SkillService(DevFreelaDbContext dbContext) {
        this._dbContext = dbContext;
    }

    public List<SkillViewModel> GetAll() {
        List<Skill> skills = this._dbContext.Skills;
        List<SkillViewModel> skillViewModel = skills
            .Select(s => new SkillViewModel(s.Id, s.Description)).ToList();
        return skillViewModel;
    }
}