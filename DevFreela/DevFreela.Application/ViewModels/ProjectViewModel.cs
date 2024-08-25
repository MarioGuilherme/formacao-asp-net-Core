namespace DevFreela.Application.ViewModels;

public class ProjectViewModel(int id, string title, DateTime createdAt) {
    public int Id { get; private set; } = id;
    public string Title { get; private set; } = title;
    public DateTime CreatedAt { get; private set; } = createdAt;
}