using API.Contracts;
using API.Contracts.Client;
using API.Controllers;
using API.Services;
using AutoFixture;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;

namespace API.Tests.Controllers
{
    public class GuestsControllerTests
    {
        private Mock<ILogger<GuestsController>> _mockLogger;
        private Mock<IGuestService> _mockGuestService;
        private Mock<IMapper> _mockMapper;
        private Mock<IValidator<Request>> _mockValidator;
        private Fixture _fixture;
        private GuestsController _sut;

        public GuestsControllerTests()
        {
            _fixture = new Fixture();

            _mockLogger = new Mock<ILogger<GuestsController>>();
            _mockGuestService = new Mock<IGuestService>();
            _mockMapper = new Mock<IMapper>();
            _mockValidator = new Mock<IValidator<Request>>();

            _sut = new GuestsController(_mockLogger.Object, _mockGuestService.Object,
                            _mockMapper.Object, _mockValidator.Object);
        }

        [Fact]
        public async Task Validate_ReturnOkResult_WhenInputIsValid_Test()
        {
            // Arrange
            Request request = _fixture.Create<Request>();
            var validationResult = _fixture.Create<ValidationResult>();
            _mockMapper.Setup(x => x.Map<Guest>(It.IsAny<Request>())).Returns(_fixture.Create<Guest>());
            _mockValidator.Setup(x => x.ValidateAsync(It.IsAny<Request>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);
            _mockGuestService.Setup(x => x.ProcessGuestInfo(It.IsAny<Guest>())).Returns(Task.CompletedTask);

            // Act
            var response = await _sut.Post(request);

            // Assert
            var okResult = Assert.IsType<Response>(response);
            response?.Message.ShouldBe("Saved");

            _mockGuestService.VerifyAll();
            _mockMapper.VerifyAll();
        }
    }
}