using Dapper;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Application.Queries.GetAllSkills;

public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<SkillViewModel>> {
    private readonly string _connectionString;

    public GetAllSkillsQueryHandler(IConfiguration configuration) {
        this._connectionString = configuration.GetConnectionString("DevFreelaCS");
    }

    public async Task<List<SkillViewModel>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken) {
        using (var sqlConnection = new SqlConnection(this._connectionString)) {
            sqlConnection.Open();
            string script = "SELECT Id, Description FROM Skills";
            var skills = await sqlConnection.QueryAsync<SkillViewModel>(script);
            return skills.ToList();
        }

        // Com EF Core
        //var skills = this._dbContext.Skills;
        //List<SkillViewModel> skillViewModel = skills
        //    .Select(s => new SkillViewModel(s.Id, s.Description)).ToList();
        //return skillViewModel;
    }
}