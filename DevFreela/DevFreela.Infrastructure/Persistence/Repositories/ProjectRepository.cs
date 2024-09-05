using Azure.Core;
using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository(DevFreelaDbContext dbContext, IConfiguration configuration) : IProjectRepository {
    private readonly DevFreelaDbContext _dbContext = dbContext;
    private readonly string _connectionString = configuration.GetConnectionString("DevFreelaCS");

    public async Task<List<Project>> GetAllAsync(string query) {
        IQueryable<Project> projects = this._dbContext.Projects;

        if (!string.IsNullOrEmpty(query))
            projects = projects.Where(p => p.Title.Contains(query) || p.Description.Contains(query));

        return await projects.ToListAsync();
    }

    public async Task<Project> GetDetailsByIdAsync(int id) {
        return await this._dbContext.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Project project) {
        await this._dbContext.Projects.AddAsync(project);
        await this._dbContext.SaveChangesAsync();
    }

    // Versão em que tira a responsabilidade de negócio (start no proj) da camada repositório (Versão do Repository)
    // Idem: Mas não é estritamente errado fazer o 'project.Start()' no repository.
    public async Task StartAsync(Project project) {
        using var sqlConnection = new SqlConnection(this._connectionString);
        sqlConnection.Open();
        string script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";
        await sqlConnection.ExecuteAsync(script, new { status = project.Status, startedAt = project.StartedAt, project.Id });
    }

    public async Task SaveChangesAsync() => await this._dbContext.SaveChangesAsync();

    public async Task AddCommentAsync(ProjectComment projectComment) {
        await this._dbContext.ProjectComments.AddAsync(projectComment);
        await this._dbContext.SaveChangesAsync();
    }
}