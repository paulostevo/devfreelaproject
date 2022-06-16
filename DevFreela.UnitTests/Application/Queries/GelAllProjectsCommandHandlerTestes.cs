using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DevFreela.UnitTests.Application.Commands
{
    public class GelAllProjectsCommandHandlerTestes
    {
        [Fact]
        public async Task ThreeProjectsExist_Executed_ReturnThreeProjectViewModels()
        {
            // Arrange
            var projects = new List<Project>
            {
                new Project("Nome do teste 1","description do teste1",1,1,90000),
                new Project("Nome do teste 2","description do teste1",1,1,70000),
                new Project("Nome do teste 3","description do teste1",1,1,80000)
            };

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(pr => pr.GetAllAsync().Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery("");
            var getAllprojectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            // Act

            var projectViewModelList = await getAllprojectsQueryHandler.Handle(getAllProjectsQuery, new CancellationToken());

            // Assert

            Assert.NotNull(projectViewModelList);
            Assert.NotEmpty(projectViewModelList);
            Assert.Equal(projects.Count, projectViewModelList.Count);

            projectRepositoryMock.Verify(pr => pr.GetAllAsync().Result, Times.Once);
        }

    }
}
