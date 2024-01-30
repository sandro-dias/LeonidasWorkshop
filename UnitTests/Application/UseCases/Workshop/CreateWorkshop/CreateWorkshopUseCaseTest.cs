using Application.Data;
using Application.Data.Specification;
using Application.UseCases.CreateWorkshop;
using Application.UseCases.CreateWorkshop.Input;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Application.UseCases.Workshop.CreateWorkshop
{
    public class CreateWorkshopUseCaseTest
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<IValidator<CreateWorkshopInput>> _validator = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<CreateWorkshopUseCase>> _logger = new();
        private CreateWorkshopUseCase _useCase;

        public CreateWorkshopUseCaseTest()
        {
            _useCase = new CreateWorkshopUseCase(_validator.Object, _unitOfWork.Object, _logger.Object);
        }

        [Fact]
        public async Task ShouldCreateWorkshopSuccessfully()
        {
            //Arrange
            var fakeInput = _fixture.Create<CreateWorkshopInput>();
            var fakeValidationResult = new ValidationResult();
            _validator.Setup(x => x.Validate(It.IsAny<CreateWorkshopInput>())).Returns(fakeValidationResult);
            _unitOfWork.Setup(x => x.WorkshopRepository.FirstOrDefaultAsync(It.IsAny<GetWorkshopByNameSpecification>(), It.IsAny<CancellationToken>()));
            _unitOfWork.Setup(x => x.WorkshopRepository.AddAsync(It.IsAny<Domain.Entities.Workshop>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.Create<Domain.Entities.Workshop>());

            //Act
            var result = await _useCase.ExecuteAsync(fakeInput);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldNotCreateWorkshopBecauseOfValidationErrors()
        {
            //Arrange
            var fakeInput = _fixture.Create<CreateWorkshopInput>();
            var fakeValidationResult = new ValidationResult();
            _validator.Setup(x => x.Validate(It.IsAny<CreateWorkshopInput>())).Returns(fakeValidationResult);
            _unitOfWork.Setup(x => x.WorkshopRepository.FirstOrDefaultAsync(It.IsAny<GetWorkshopByNameSpecification>(), It.IsAny<CancellationToken>()));
            _unitOfWork.Setup(x => x.WorkshopRepository.AddAsync(It.IsAny<Domain.Entities.Workshop>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.Create<Domain.Entities.Workshop>());

            //Act
            var result = await _useCase.ExecuteAsync(fakeInput);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldNotCreateWorkshopBecauseWorkshopAlreadyExists()
        {
            //Arrange
            var fakeInput = _fixture.Create<CreateWorkshopInput>();
            var fakeValidationFailure = new ValidationFailure("Workload", "A carga de trabalha precisa existir e ser maior que 0");
            var fakeValidationResult = new ValidationResult(new List<ValidationFailure>() { fakeValidationFailure });
            _validator.Setup(x => x.Validate(It.IsAny<CreateWorkshopInput>())).Returns(fakeValidationResult);

            //Act
            var result = await _useCase.ExecuteAsync(fakeInput);

            //Assert
            result.Should().BeNull();

            _unitOfWork.Verify(x => x.WorkshopRepository.FirstOrDefaultAsync(It.IsAny<GetWorkshopByNameSpecification>(), It.IsAny<CancellationToken>()), Times.Never);
            _unitOfWork.Verify(x => x.WorkshopRepository.AddAsync(It.IsAny<Domain.Entities.Workshop>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
