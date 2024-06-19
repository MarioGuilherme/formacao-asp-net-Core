namespace DevFreela.Core.Entities;

public class User : BaseEntity {
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool Active { get; private set; }
    public List<UserSkill> Skills { get; private set; }
    public List<Project> OwnedProjects { get; private set; }
    public List<Project> FreelanceProjects { get; private set; }

    public User(string fullName, string email, DateTime birthDate) {
        this.FullName = fullName;
        this.Email = email;
        this.BirthDate = birthDate;
        this.Active = true;
        this.CreatedAt = DateTime.Now;
        this.Skills = new List<UserSkill>();
        this.OwnedProjects = new List<Project>();
        this.FreelanceProjects = new List<Project>();
    }
}