using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations;

public class ProjectService : IProjectService {
    private readonly DevFreelaDbContext _dbContext;

    public ProjectService(DevFreelaDbContext dbContext) {
        this._dbContext = dbContext;
    }

    public int Create(NewProjectInputModel inputModel) {
        Project project = new(inputModel.Title, inputModel.Description, inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost);
        this._dbContext.Projects.Add(project);
        return project.Id;
    }

    public void CreateComment(CreateCommentInputModel inputModel) {
        ProjectComment projectComment = new(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
        this._dbContext.ProjectComments.Add(projectComment);
    }

    public void Delete(int id) {
        Project project = this._dbContext.Projects.SingleOrDefault(p =>  p.Id == id);
        project.Cancel();
    }

    public void Finish(int id) {
        Project project = this._dbContext.Projects.SingleOrDefault(p => p.Id == id);
        project.Finish();
    }

    public List<ProjectViewModel> GetAll(string query) {
        List<Project> projects = this._dbContext.Projects;
        List<ProjectViewModel> projectViewModels = projects
            .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt)).ToList();
        return projectViewModels;
    }

    public ProjectDetailsViewModel GetById(int id) {
        Project project = this._dbContext.Projects.SingleOrDefault(p => p.Id == id);

        if (project == null)
            return null;

        ProjectDetailsViewModel projectDetailsViewModel = new(
            project.Id,
            project.Title,
            project.Description,
            project.TotalCost,
            project.StartedAt,
            project.FinishedAt
        );
        return projectDetailsViewModel;
    }

    public void Start(int id) {
        Project project = this._dbContext.Projects.SingleOrDefault(p => p.Id == id);
        project.Start();
    }

    public void Update(UpdateProjectInputModel inputModel) {
        Project project = this._dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);
        project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
    }
}