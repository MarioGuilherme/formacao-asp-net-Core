using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities;

public class Project : BaseEntity {
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int IdClient { get; private set; }
    public int IdFreelancer { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }
    public ProjectStatusEnum Status { get; private set; }
    public List<ProjectComment> Comments { get; private set; }

    public Project(string title, string description, int idClient, int idFreelancer, decimal totalCost) {
        this.Title = title;
        this.Description = description;
        this.IdClient = idClient;
        this.IdFreelancer = idFreelancer;
        this.TotalCost = totalCost;

        this.CreatedAt = DateTime.Now;
        this.Status = ProjectStatusEnum.Created;
        this.Comments = new List<ProjectComment>();
    }

    public void Cancel() {
        if (this.Status == ProjectStatusEnum.InProgress)
            this.Status = ProjectStatusEnum.Cancelled;
    }

    public void Finish() {
        if (this.Status == ProjectStatusEnum.InProgress) {
            this.Status = ProjectStatusEnum.Finished;
            this.FinishedAt = DateTime.Now;
        }
    }

    public void Start() {
        if (this.Status == ProjectStatusEnum.Created) {
            this.Status = ProjectStatusEnum.InProgress;
            this.StartedAt = DateTime.Now;
        }
    }

    public void Update(string title, string description, decimal totalCost) {
        this.Title = title;
        this.Description = description;
        this.TotalCost = totalCost;
    }
}