﻿namespace DevFreela.Application.ViewModels;

public class ProjectDetailsViewModel {
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? FinishedAt { get; private set; }

    public ProjectDetailsViewModel(int id, string title, string description, decimal totalCost, DateTime? startedAt, DateTime? finishedAt) {
        this.Id = id;
        this.Title = title;
        this.Description = description;
        this.TotalCost = totalCost;
        this.StartedAt = startedAt;
        this.FinishedAt = finishedAt;
    }
}