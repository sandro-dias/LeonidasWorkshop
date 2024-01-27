using Application.Data;
using Application.Data.Specification;
using Application.UseCases.GetWorkshopWorkload;
using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Application.UseCases.Workshop
{
    public class GetWorkshopWorkloadUseCaseTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<GetWorkshopWorkloadUseCase>> _logger = new();
        private readonly GetWorkshopWorkloadUseCase _useCase;
        private readonly Fixture _fixture = new();

        public GetWorkshopWorkloadUseCaseTest()
        {
            _useCase = new GetWorkshopWorkloadUseCase(_unitOfWork.Object, _logger.Object);
        }

        [Fact]
        public async Task ShouldGetWorkshopWorkloadSuccessfully()
        {
            //Arrange
            var fakeWorkshopId = _fixture.Create<long>();
            var fakeWorkshop = _fixture.Create<Domain.Entities.Workshop>();
            var fakeWorkingDayList = _fixture.CreateMany<WorkingDay>().ToList();

            _unitOfWork.Setup(x => x.WorkshopRepository.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>())).ReturnsAsync(fakeWorkshop);
            _unitOfWork.Setup(x => x.WorkingDayRepository.ListAsync(It.IsAny<GetWorkingDayByDateRangeSpecification>(), It.IsAny<CancellationToken>())).ReturnsAsync(fakeWorkingDayList);

            //Act
            var result = await _useCase.ExecuteAsync(fakeWorkshopId);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldNotGetWorkshopWorkloadBecauseWorkshopIsNull()
        {
            //Arrange
            var fakeWorkshopId = _fixture.Create<long>();
            var fakeWorkshop = _fixture.Create<Domain.Entities.Workshop>();
            var fakeWorkingDayList = _fixture.CreateMany<WorkingDay>().ToList();

            _unitOfWork.Setup(x => x.WorkshopRepository.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()));

            //Act
            var result = await _useCase.ExecuteAsync(fakeWorkshopId);

            //Assert
            result.Should().BeNull();
            _unitOfWork.Verify(x => x.WorkingDayRepository.ListAsync(It.IsAny<GetWorkingDayByDateRangeSpecification>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Theory]
        [InlineData(22, 01, 2024, 29, 01, 2024)]
        [InlineData(23, 01, 2024, 30, 01, 2024)]
        [InlineData(24, 01, 2024, 31, 01, 2024)]
        [InlineData(25, 01, 2024, 01, 02, 2024)]
        [InlineData(26, 01, 2024, 02, 02, 2024)]
        public void ShouldGetEndCorrectly(int initialDay, int initialMonth, int initialYear, int endDay, int endMonth, int endYear)
        {
            //Arrange
            var initialDate = new DateTime(initialYear, initialMonth, initialDay);

            //Act
            var result = _useCase.GetEndDate(initialDate);

            //Assert
            result.Date.Should().Be(new DateTime(endYear, endMonth, endDay));
        }
    }
}
