using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities;

public class Project(string title, string description, int idClient, int idFreelancer, decimal totalCost) : BaseEntity {
    public string Title { get; private set; } = title;
    public string Description { get; private set; } = description;
    public int IdClient { get; private set; } = idClient;
    public User Client { get; private set; }
    public int IdFreelancer { get; private set; } = idFreelancer;
    public User Freelancer { get; private set; }
    public decimal TotalCost { get; private set; } = totalCost;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime? StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }
    public ProjectStatusEnum Status { get; private set; } = ProjectStatusEnum.Created;
    public List<ProjectComment> Comments { get; private set; } = [];

    public void Cancel() {
        if (this.Status == ProjectStatusEnum.InProgress)
            this.Status = ProjectStatusEnum.Cancelled;
    }

    public void Finish() {
        if (this.Status == ProjectStatusEnum.PaymentPending) {
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

    public void SetPaymentPending() {
        this.Status = ProjectStatusEnum.PaymentPending;
        this.FinishedAt = null;
    }

    public void Update(string title, string description, decimal totalCost) {
        this.Title = title;
        this.Description = description;
        this.TotalCost = totalCost;
    }
}