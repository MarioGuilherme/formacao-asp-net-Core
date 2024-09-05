using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries;

public class GetAllProjectsCommandHandlerTests {
    [Fact]
    public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels() {
        // Arrange
        PaginationResult<Project> projects = new() {
            Data = [
                new("Nome de Teste 1", "Descrição de Teste 1", 1, 2, 10000),
                new("Nome de Teste 2", "Descrição de Teste 2", 1, 2, 20000),
                new("Nome de Teste 3", "Descrição de Teste 3", 1, 2, 30000)
            ]
        };

        Mock<IProjectRepository> projectRepositoryMock = new();
        projectRepositoryMock.Setup(pr => pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result).Returns(projects);

        GetAllProjectsQuery getAllProjectsQuery = new() { Query = string.Empty, Page = 1 };
        GetAllProjectsQueryHandler getAllProjectsQueryHandler = new(projectRepositoryMock.Object);

        // Act
        PaginationResult<ProjectViewModel> paginationProjectViewModelList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, new());

        // Assert
        Assert.NotNull(paginationProjectViewModelList);
        Assert.NotEmpty(paginationProjectViewModelList.Data);
        Assert.Equal(projects.Data.Count, paginationProjectViewModelList.Data.Count);

        projectRepositoryMock.Verify(pr => pr.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result, Times.Once);
    }
}