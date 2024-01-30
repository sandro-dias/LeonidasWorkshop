using Application.Data;
using Application.Data.Specification;
using Application.UseCases.Customer.CreateCustomer;
using Application.UseCases.Customer.CreateCustomer.Input;
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

namespace UnitTests.Application.UseCases.Customer
{
    public class CreateCustomerUseCaseTest
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<IValidator<CreateCustomerInput>> _validator = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ILogger<CreateCustomerUseCase>> _logger = new();
        private readonly CreateCustomerUseCase _useCase;

        public CreateCustomerUseCaseTest()
        {
            _useCase = new CreateCustomerUseCase(_validator.Object, _unitOfWork.Object, _logger.Object);
        }

        [Fact]
        public async Task ShouldCreateCustomerSuccessfully()
        {
            //Arrange
            var fakeInput = _fixture.Create<CreateCustomerInput>();
            var fakeValidationResult = new ValidationResult();
            _validator.Setup(x => x.Validate(It.IsAny<CreateCustomerInput>())).Returns(fakeValidationResult);
            _unitOfWork.Setup(x => x.CustomerRepository.FirstOrDefaultAsync(It.IsAny<GetCustomerByCPFSpecification>(), It.IsAny<CancellationToken>()));
            _unitOfWork.Setup(x => x.CustomerRepository.AddAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()));
            _unitOfWork.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()));

            //Act
            var result = await _useCase.ExecuteAsync(fakeInput);

            //Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(fakeInput.Name);
        }

        [Fact]
        public async Task ShouldNotCreateCustomerBecauseOfValidationErrors()
        {
            //Arrange
            var fakeInput = _fixture.Create<CreateCustomerInput>();
            var fakeValidationFailure = new ValidationFailure("CPF", "O CPF inserido não tem os caracteres esperados");
            var fakeValidationResult = new ValidationResult(new List<ValidationFailure>() { fakeValidationFailure });
            _validator.Setup(x => x.Validate(It.IsAny<CreateCustomerInput>())).Returns(fakeValidationResult);
            _unitOfWork.Setup(x => x.CustomerRepository.FirstOrDefaultAsync(It.IsAny<GetCustomerByCPFSpecification>(), It.IsAny<CancellationToken>()));
            _unitOfWork.Setup(x => x.CustomerRepository.AddAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()));
            _unitOfWork.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()));

            //Act
            var result = await _useCase.ExecuteAsync(fakeInput);

            //Assert
            result.Should().BeNull();
            _unitOfWork.Verify(x => x.CustomerRepository.FirstOrDefaultAsync(It.IsAny<GetCustomerByCPFSpecification>(), It.IsAny<CancellationToken>()), Times.Never);
            _unitOfWork.Verify(x => x.CustomerRepository.AddAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task ShouldNotCreateCustomerBecauseItAlreadyExists()
        {
            //Arrange
            var fakeInput = _fixture.Create<CreateCustomerInput>();
            var fakeValidationResult = new ValidationResult();
            _validator.Setup(x => x.Validate(It.IsAny<CreateCustomerInput>())).Returns(fakeValidationResult);
            _unitOfWork.Setup(x => x.CustomerRepository.FirstOrDefaultAsync(It.IsAny<GetCustomerByCPFSpecification>(), It.IsAny<CancellationToken>())).ReturnsAsync(_fixture.Create<Domain.Entities.Customer>());
            _unitOfWork.Setup(x => x.CustomerRepository.AddAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()));
            _unitOfWork.Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()));

            //Act
            var result = await _useCase.ExecuteAsync(fakeInput);

            //Assert
            result.Should().NotBeNull();
            _unitOfWork.Verify(x => x.CustomerRepository.AddAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()), Times.Never);
            _unitOfWork.Verify(x => x.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
