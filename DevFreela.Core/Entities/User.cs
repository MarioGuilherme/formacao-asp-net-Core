namespace DevFreela.Core.Entities;

public class User : BaseEntity {
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool Active { get; private set; }
    public string Password { get; private set; }
    public string Role { get; private set; }
    public List<UserSkill> Skills { get; private set; }
    public List<Project> OwnedProjects { get; private set; }
    public List<Project> FreelanceProjects { get; private set; }
    public List<ProjectComment> Comments { get; private set; }

    public User(string fullName, string email, DateTime birthDate, string password, string role) {
        this.FullName = fullName;
        this.Email = email;
        this.BirthDate = birthDate;
        this.Active = true;
        this.Password = password;
        this.Role = role;
        this.CreatedAt = DateTime.Now;
        this.Skills = new List<UserSkill>();
        this.OwnedProjects = new List<Project>();
        this.FreelanceProjects = new List<Project>();
    }
}