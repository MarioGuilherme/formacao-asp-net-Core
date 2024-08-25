using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries;

public class GetAllProjectsCommandHandlerTests {
    [Fact]
    public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels() {
        // Arrange
        List<Project> projects = [
            new("Nome de Teste 1", "Descrição de Teste 1", 1, 2, 10000),
            new("Nome de Teste 2", "Descrição de Teste 2", 1, 2, 20000),
            new("Nome de Teste 3", "Descrição de Teste 3", 1, 2, 30000)
        ];

        Mock<IProjectRepository> projectRepositoryMock = new();
        projectRepositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(projects);

        GetAllProjectsQuery getAllProjectsQuery = new(string.Empty);
        GetAllProjectsQueryHandler getAllProjectsQueryHandler = new(projectRepositoryMock.Object);

        // Act
        List<ProjectViewModel> projectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new());

        // Assert
        Assert.NotNull(projectViewModelList);
        Assert.NotEmpty(projectViewModelList);
        Assert.Equal(projects.Count, projectViewModelList.Count);

        projectRepositoryMock.Verify(pr => pr.GetAllAsync().Result, Times.Once);
    }
}