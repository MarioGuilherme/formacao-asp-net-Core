using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, Unit> {
    private readonly DevFreelaDbContext _dbContext;
    private readonly string _connectionString;

    public StartProjectCommandHandler(DevFreelaDbContext dbContext, IConfiguration configuration) {
        _dbContext = dbContext;
        _connectionString = configuration.GetConnectionString("DevFreelaCs");
    }

    public async Task<Unit> Handle(StartProjectCommand request, CancellationToken cancellationToken) {
        Project project = await this._dbContext.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);
        project.Start();
        //this._dbContext.SaveChanges();
        using (var sqlConnection = new SqlConnection(this._connectionString)) {
            sqlConnection.Open();
            string script = "UPDATE Projects SET Status = @status, StartedAt = @startedAt WHERE Id = @id";
            sqlConnection.Execute(script, new { status = project.Status, startedAt = project.StartedAt, request.Id });
        }
        return Unit.Value;
    }
}
