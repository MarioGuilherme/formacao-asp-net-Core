using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllSkills;

public class GetAllSkillsQueryHandler(ISkillRepository skillRepository) : IRequestHandler<GetAllSkillsQuery, List<SkillViewModel>> {
    private readonly ISkillRepository _skillRepository = skillRepository;

    public async Task<List<SkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken) {
        List<Skill> skills = await this._skillRepository.GetAllAsync();

        List<SkillViewModel> skillsViewModel = skills
            .Select(s => new SkillViewModel(s.Id, s.Description))
            .ToList();

        return skillsViewModel;
    }
}