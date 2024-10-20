using MediatR;

namespace DevFreela.Application.Notification.ProjectCreated;

public class ProjectCreatedNotification(int id, string title, decimal totalCost) : INotification {
    public int Id { get; private set; } = id;
    public string Title { get; private set; } = title;
    public decimal TotalCost { get; private set; } = totalCost;
}