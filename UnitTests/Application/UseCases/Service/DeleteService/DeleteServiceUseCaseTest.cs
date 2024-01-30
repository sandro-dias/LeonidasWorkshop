using Application.Data;
using Application.Data.Specification;
using Application.UseCases.Service.DeleteService;
using AutoFixture;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Application.UseCases.Service.DeleteService
{
    public class DeleteServiceUseCaseTest
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<DeleteServiceUseCase>> _logger = new();
        private readonly DeleteServiceUseCase _useCase;

        public DeleteServiceUseCaseTest()
        {
            _useCase = new DeleteServiceUseCase(_unitOfWork.Object, _logger.Object);
        }

        [Fact]
        public async Task ShouldDeleteServiceSuccessfully()
        {
            //Arrange
            var fakeServiceId = _fixture.Create<long>();
            _unitOfWork.Setup(x => x.ServiceRepository.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.Create<Domain.Entities.Service>());
            _unitOfWork.Setup(x => x.WorkingDayRepository.FirstOrDefaultAsync(It.IsAny<GetWorkingDayByWorkshopSpecification>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.Create<Domain.Entities.WorkingDay>());
            _unitOfWork.Setup(x => x.ServiceRepository.DeleteAsync(It.IsAny<Domain.Entities.Service>(), It.IsAny<CancellationToken>()));
            _unitOfWork.Setup(x => x.WorkingDayRepository.UpdateAsync(It.IsAny<WorkingDay>(), It.IsAny<CancellationToken>()));
            _unitOfWork.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()));

            //Act
            await _useCase.ExecuteAsync(fakeServiceId);

            //Assert
            _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ShouldNotDeleteServiceBecauseServiceDoesNotExist()
        {
            //Arrange
            var fakeServiceId = _fixture.Create<long>();
            _unitOfWork.Setup(x => x.ServiceRepository.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()));

            //Act
            await _useCase.ExecuteAsync(fakeServiceId);

            //Assert
            _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
