using Moq;

using Severstal.Core;
using Severstal.Core.Contracts;

namespace Severstal.Core
{
    public class ServiceUnitTests
    {
        [Fact]
        public async Task TestTasksConnectingRepositoryCalledOnce()
        {
            // Arrange
            var repositoryMock = new Mock<IRepository>();
            var exportMock = new Mock<IExport>();
            var Service = new Service(repositoryMock.Object, exportMock.Object);

            // Act
            await Service.startDataProcessing();

            // Assert
            repositoryMock.Verify(r => r.GetTaskLoadReportAsync(), Times.Once);
        }
    }
}