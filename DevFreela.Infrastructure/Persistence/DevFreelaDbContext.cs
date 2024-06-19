using DevFreela.Core.Entities;

namespace DevFreela.Infrastructure.Persistence;

public class DevFreelaDbContext {
    public DevFreelaDbContext() {
        this.Projects = new List<Project> {
            new Project("Meu Projeto ASPNET Core 1", "Minha descrição de projeto 1", 1, 1, 1000),
            new Project("Meu Projeto ASPNET Core 2", "Minha descrição de projeto 2", 1, 1, 365),
            new Project("Meu Projeto ASPNET Core 3", "Minha descrição de projeto 3", 1, 1, 9789),
        };
        this.Users = new List<User> {
            new User("Mário Guilherme", "marioguilhermedev@gmail.com", new DateTime(2003, 8, 21)),
            new User("Carlos Guilherme", "lucas@gmail.com", new DateTime(1998, 8, 21)),
            new User("Lucas Guilherme", "carlos@gmail.com", new DateTime(1985, 8, 21))
        };
        this.Skills = new List<Skill> {
            new Skill(".NET Core"),
            new Skill("C#"),
            new Skill("SQL")
        };
    }

    public List<Project> Projects { get; set; }
    public List<User> Users { get; set; }
    public List<Skill> Skills { get; set; }
    public List<ProjectComment> ProjectComments { get; set; }
}