using API.Contracts;
using API.Repositories;
using API.Services;
using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;

namespace API.Tests.Services
{
    public class GuestServiceTests
    {
        private Mock<ILogger<GuestService>> _mockLogger;
        private Mock<IDataManager> _mockDataManager;
        private Fixture _fixture;
        private GuestService _sut;

        public GuestServiceTests()
        {
            _fixture = new Fixture();

            _mockLogger = new Mock<ILogger<GuestService>>();
            _mockDataManager = new Mock<IDataManager>();

            _sut = new GuestService(_mockDataManager.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task ProcessGuestInfo_Successful_Test()
        {
            // Arrange
            _mockDataManager.Setup(x => x.SaveGuest(It.IsAny<Guest>())).Returns(Task.CompletedTask);

            // Act
            await _sut.ProcessGuestInfo(_fixture.Create<Guest>());

            // Verify
            _mockLogger
                .VerifyLogging("Start processing guest information ......")
                .VerifyLogging("Notify administrator......");
        }
    }
}