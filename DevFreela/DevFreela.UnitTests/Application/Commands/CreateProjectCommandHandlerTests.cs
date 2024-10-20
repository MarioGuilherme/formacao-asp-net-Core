using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Moq;

namespace DevFreela.UnitTests.Application.Commands;

public class CreateProjectCommandHandlerTests {
    [Fact]
    public async Task InputDataIsOk_Executed_ReturnProjectId() {
        // Arrange
        Mock<IUnitOfWork> unitOfWork = new();
        Mock<IMediator> mediator = new();
        Mock<IProjectRepository> projectRepositoryMock = new();
        Mock<ISkillRepository> skillRepositoryMock = new();

        unitOfWork.SetupGet(uow => uow.Projects).Returns(projectRepositoryMock.Object);
        unitOfWork.SetupGet(uow => uow.Skills).Returns(skillRepositoryMock.Object);

        CreateProjectCommand createProjectCommand = new() {
            Title = "Título de Teste",
            Description = "Uma descrição top de linha",
            TotalCost = 30_000,
            IdClient = 1,
            IdFreelancer = 2
        };

        CreateProjectCommandHandler createProjectCommandHandler = new(unitOfWork.Object, mediator.Object);

        // Act
        int id = await createProjectCommandHandler.Handle(createProjectCommand, new());

        // Assert
        Assert.True(id >= 0);

        projectRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
    }
}