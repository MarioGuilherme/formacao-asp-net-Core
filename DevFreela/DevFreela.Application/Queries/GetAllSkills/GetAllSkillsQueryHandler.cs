using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllSkills;

public class GetAllSkillsQueryHandler(ISkillRepository skillRepository) : IRequestHandler<GetAllSkillsQuery, List<SkillDTO>> {
    private readonly ISkillRepository _skillRepository = skillRepository;

    public async Task<List<SkillDTO>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken) => await this._skillRepository.GetAllAsync();
}