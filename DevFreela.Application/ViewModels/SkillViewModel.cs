namespace DevFreela.Application.ViewModels;

public class SkillViewModel(int id, string description) {
    public int Id { get; private set; } = id;
    public string Description { get; private set; } = description;
}