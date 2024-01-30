using Application.Data;
using Application.Services.CreateWorkingDay;
using Application.Services.CreateWorkingDay.Input;
using Application.UseCases.Service.CreateService;
using Application.UseCases.Service.CreateService.Input;
using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Application.UseCases.Service.CreateService
{
    public class CreateServiceUseCaseTest
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<CreateServiceUseCase>> _logger = new();
        private readonly Mock<ICreateWorkingDayService> _createWorkingDayService = new();
        private readonly CreateServiceUseCase _useCase;

        public CreateServiceUseCaseTest()
        {
            _useCase = new CreateServiceUseCase(_unitOfWork.Object, _logger.Object, _createWorkingDayService.Object);
        }

        [Fact]
        public async Task ShouldCreateServiceSuccesfully()
        {
            //Arrange
            var fakeInput = _fixture.Create<CreateServiceInput>();
            var fakeWorkshop = _fixture.Create<Domain.Entities.Workshop>();
            _unitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.Create<Domain.Entities.Customer>());
            _unitOfWork.Setup(x => x.WorkshopRepository.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>())).ReturnsAsync(fakeWorkshop);
            _createWorkingDayService.Setup(x => x.CreateWorkingDay(It.IsAny<CreateWorkingDayInput>())).ReturnsAsync(CreateWorkingDay(fakeWorkshop.WorkShopId, fakeInput.Date, availableWorkload : 100));
            _unitOfWork.Setup(x => x.ServiceRepository.AddAsync(It.IsAny<Domain.Entities.Service>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.Create<Domain.Entities.Service>());
            _unitOfWork.Setup(x => x.WorkingDayRepository.UpdateAsync(It.IsAny<WorkingDay>(), It.IsAny<CancellationToken>()));
            _unitOfWork.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()));

            //Act
            var result = await _useCase.ExecuteAsync(fakeInput);

            //Assert
            result.Should().NotBeNull();
            _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ShouldNotCreateServiceBecauseCustomerDoesNotExist()
        {
            //Arrange
            var fakeInput = _fixture.Create<CreateServiceInput>();
            _unitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()));

            //Act
            var result = await _useCase.ExecuteAsync(fakeInput);

            //Assert
            result.Should().BeNull();
            _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task ShouldNotCreateServiceBecauseWorkshopDoesNotExist()
        {
            //Arrange
            var fakeInput = _fixture.Create<CreateServiceInput>();
            var fakeWorkshop = _fixture.Create<Domain.Entities.Workshop>();
            _unitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.Create<Domain.Entities.Customer>());
            _unitOfWork.Setup(x => x.WorkshopRepository.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()));

            //Act
            var result = await _useCase.ExecuteAsync(fakeInput);

            //Assert
            result.Should().BeNull();
            _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task ShouldNotCreateServiceBecauseWorkingDayCannotBeCreated()
        {
            //Arrange
            var fakeInput = _fixture.Create<CreateServiceInput>();
            var fakeWorkshop = _fixture.Create<Domain.Entities.Workshop>();
            _unitOfWork.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.Create<Domain.Entities.Customer>());
            _unitOfWork.Setup(x => x.WorkshopRepository.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>())).ReturnsAsync(fakeWorkshop);
            _createWorkingDayService.Setup(x => x.CreateWorkingDay(It.IsAny<CreateWorkingDayInput>()));

            //Act
            var result = await _useCase.ExecuteAsync(fakeInput);

            //Assert
            result.Should().BeNull();
            _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Theory]
        [InlineData(10, 8, true)]
        [InlineData(8, 8, true)]
        [InlineData(5, 8, false)]
        public void ValidateAvailableWorkload(int availableWorkload, int serviceWorkload, bool expectedResult) => _useCase.IsAvailableWorkloadGreaterThanServiceWorkload(availableWorkload, serviceWorkload).Should().Be(expectedResult);

        private WorkingDay CreateWorkingDay(long workshopId, DateTime date, int availableWorkload)
        {
            return WorkingDay.CreateWorkingDay(workshopId, date, availableWorkload);
        }
    }
}
