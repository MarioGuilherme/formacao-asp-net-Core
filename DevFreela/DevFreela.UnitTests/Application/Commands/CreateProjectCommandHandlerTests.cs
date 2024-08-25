using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Commands;

public class CreateProjectCommandHandlerTests {
    [Fact]
    public async Task InputDataIsOk_Executed_ReturnProjectId() {
        // Arrange
        Mock<IProjectRepository> projectRepositoryMock = new();

        CreateProjectCommand createProjectCommand = new() {
            Title = "Título de Teste",
            Description = "Uma descrição top de linha",
            TotalCost = 30_000,
            IdClient = 1,
            IdFreelancer = 2
        };

        CreateProjectCommandHandler createProjectCommandHandler = new(projectRepositoryMock.Object);

        // Act
        int id = await createProjectCommandHandler.Handle(createProjectCommand, new());

        // Assert
        Assert.True(id >= 0);

        projectRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
    }
}