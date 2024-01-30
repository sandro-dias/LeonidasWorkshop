using Application.Data;
using Application.Data.Specification;
using Application.UseCases.Service.GetServices;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Application.UseCases.Service.GetServices
{
    public class GetServiceUseCaseTest
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<GetServicesUseCase>> _logger = new();
        private readonly Mock<IMemoryCache> _memoryCache = new();
        private GetServicesUseCase _useCase;

        public GetServiceUseCaseTest()
        {
            _useCase = new GetServicesUseCase(_unitOfWork.Object, _logger.Object, _memoryCache.Object); 
        }

        [Fact]
        public async Task ShouldGetServicesSuccessfully()
        {
            //Arrange
            var fakeWorkshopId = _fixture.Create<long>();
            _unitOfWork.Setup(x => x.WorkshopRepository.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.Create<Domain.Entities.Workshop>());
            MockMemoryCacheEmpty();
            _unitOfWork.Setup(x => x.ServiceRepository.ListAsync(It.IsAny<GetTodayServicesByWorkshopIdSpecification>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.CreateMany<Domain.Entities.Service>());            

            //Act
            var result = await _useCase.ExecuteAsync(fakeWorkshopId);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldGetServicesSuccessfullyInMemoryCache()
        {
            //Arrange
            var fakeWorkshopId = _fixture.Create<long>();
            _unitOfWork.Setup(x => x.WorkshopRepository.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.Create<Domain.Entities.Workshop>());
            SetUseCaseWithMemoryCache(GetMemoryCache(_fixture.CreateMany<Domain.Entities.Service>()));

            //Act
            var result = await _useCase.ExecuteAsync(fakeWorkshopId);

            //Assert
            result.Should().NotBeNull();
            _unitOfWork.Verify(x => x.ServiceRepository.ListAsync(It.IsAny<GetTodayServicesByWorkshopIdSpecification>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task ShouldNotGetServicesSuccessfullyBecauseWorkshopDoesNotExist()
        {
            //Arrange
            var fakeWorkshopId = _fixture.Create<long>();
            _unitOfWork.Setup(x => x.WorkshopRepository.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()));

            //Act
            var result = await _useCase.ExecuteAsync(fakeWorkshopId);

            //Assert
            result.Should().BeEmpty();
            _unitOfWork.Verify(x => x.ServiceRepository.ListAsync(It.IsAny<GetTodayServicesByWorkshopIdSpecification>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        private void SetUseCaseWithMemoryCache(IMemoryCache memoryCache)
        {
            _useCase = new GetServicesUseCase(_unitOfWork.Object, _logger.Object, memoryCache);
        }

        private static IMemoryCache GetMemoryCache(object expectedValue)
        {
            var mockMemoryCache = new Mock<IMemoryCache>();
            mockMemoryCache.Setup(x => x.TryGetValue(It.IsAny<object>(), out expectedValue)).Returns(true);
            return mockMemoryCache.Object;
        }

        private void MockMemoryCacheEmpty()
        {
            var cacheEntry = Mock.Of<ICacheEntry>();
            Mock.Get(cacheEntry).SetupGet(x => x.ExpirationTokens).Returns(new List<IChangeToken>());

            _memoryCache.Setup(x => x.CreateEntry(It.IsAny<object>())).Returns(cacheEntry);
        }
    }
}
