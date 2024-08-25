namespace DevFreela.Core.Entities;

public class User(string fullName, string email, DateTime birthDate, string password, string role) : BaseEntity {
    public string FullName { get; private set; } = fullName;
    public string Email { get; private set; } = email;
    public DateTime BirthDate { get; private set; } = birthDate;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public bool Active { get; private set; } = true;
    public string Password { get; private set; } = password;
    public string Role { get; private set; } = role;
    public List<UserSkill> Skills { get; private set; } = [];
    public List<Project> OwnedProjects { get; private set; } = [];
    public List<Project> FreelanceProjects { get; private set; } = [];
    public List<ProjectComment> Comments { get; private set; }
}