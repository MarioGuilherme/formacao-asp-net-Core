using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class SkillRepository(IConfiguration configuration) : ISkillRepository {
    private readonly string _connectionString = configuration.GetConnectionString("DevFreelaCS");

    public async Task<List<SkillDTO>> GetAllAsync() {
        using var sqlConnection = new SqlConnection(this._connectionString);
        sqlConnection.Open();
        string script = "SELECT Id, Description FROM Skills";
        IEnumerable<SkillDTO> skills = await sqlConnection.QueryAsync<SkillDTO>(script); // Neste caso aqui, já é feito a projeção para um DTO (dentro do repository)
        return skills.ToList();

        // Com EF Core
        //var skills = this._dbContext.Skills;
        //List<SkillViewModel> skillViewModel = skills
        //    .Select(s => new SkillViewModel(s.Id, s.Description)).ToList();
        //return skillViewModel;
    }
}