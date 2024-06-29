using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implementations;

public class ProjectService : IProjectService {
    private readonly DevFreelaDbContext _dbContext;
    private readonly string _connectionString;

    public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration) {
        this._dbContext = dbContext;
        this._connectionString = configuration.GetConnectionString("DevFreelaCS");
    }

    public int Create(NewProjectInputModel inputModel) {
        Project project = new(inputModel.Title, inputModel.Description, inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost);
        this._dbContext.Projects.Add(project);
        this._dbContext.SaveChanges();
        return project.Id;
    }

    public void CreateComment(CreateCommentInputModel inputModel) {
        ProjectComment projectComment = new(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
        this._dbContext.ProjectComments.Add(projectComment);
        this._dbContext.SaveChanges();
    }

    public void Delete(int id) {
        Project project = this._dbContext.Projects.SingleOrDefault(p =>  p.Id == id);
        project.Cancel();
        this._dbContext.SaveChanges();
    }

    public void Finish(int id) {
        Project project = this._dbContext.Projects.SingleOrDefault(p => p.Id == id);
        project.Finish();
        this._dbContext.SaveChanges();
    }

    public List<ProjectViewModel> GetAll(string query) {
        var projects = this._dbContext.Projects;
        List<ProjectViewModel> projectViewModels = projects
            .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt)).ToList();
        return projectViewModels;
    }

    public ProjectDetailsViewModel GetById(int id) {
        Project project = this._dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .SingleOrDefault(p => p.Id == id);

        if (project == null)
            return null;

        ProjectDetailsViewModel projectDetailsViewModel = new(
            project.Id,
            project.Title,
            project.Description,
            project.TotalCost,
            project.StartedAt,
            project.FinishedAt,
            project.Client.FullName,
            project.Freelancer.FullName
        );
        return projectDetailsViewModel;
    }

    public void Start(int id) {
        Project project = this._dbContext.Projects.SingleOrDefault(p => p.Id == id);
        project.Start();
        //this._dbContext.SaveChanges();
        using (var sqlConnection = new SqlConnection(this._connectionString)) {
            sqlConnection.Open();
            string script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";
            sqlConnection.Execute(script, new { status = project.Status, startedAt = project.StartedAt, id });
        }
    }

    public void Update(UpdateProjectInputModel inputModel) {
        Project project = this._dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);
        project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
        this._dbContext.SaveChanges();
    }
}