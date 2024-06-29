using Dapper;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Services.Implementations;

public class SkillService : ISkillService {
    private readonly DevFreelaDbContext _dbContext;
    private readonly string _connectionString;

    public SkillService(DevFreelaDbContext dbContext, IConfiguration configuration) {
        this._dbContext = dbContext;
        this._connectionString = configuration.GetConnectionString("DevFreelaCS");
    }

    public List<SkillViewModel> GetAll() {
        using (var sqlConnection = new SqlConnection(this._connectionString)) {
            sqlConnection.Open();
            string script = "SELECT Id, Description FROM Skills";
            return sqlConnection.Query<SkillViewModel>(script).ToList();
        }
        //var skills = this._dbContext.Skills;
        //List<SkillViewModel> skillViewModel = skills
        //    .Select(s => new SkillViewModel(s.Id, s.Description)).ToList();
        //return skillViewModel;
    }
}