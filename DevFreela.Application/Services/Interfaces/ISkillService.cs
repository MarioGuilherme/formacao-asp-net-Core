using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Services.Interfaces;

internal interface ISkillService {
    List<SkillViewModel> GetAll();
}