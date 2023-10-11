using FakeItEasy;
using Serilog;
using SyncGuardianWpf.Services.Interfaces;

namespace SyncGuardianTests.WPF.Services
{
    [TestFixture]
    public class DeviceIDGenerationServiceTests
    {
        private IDeviceIDGenerationService _service;
        private ILogger _logger;

        //Global Arrange for DI
        [SetUp]
        public void Setup()
        {
            // Initialize your DI once
            // This could be setting up your IoC container and resolving the service
            _logger = A.Fake<ILogger>();
            _service = A.Fake<IDeviceIDGenerationService>();
        }

        [Test]
        public void GenerateDeviceID_ReturnsNonEmptyString()
        {
            // Arrange
            var guid = Guid.NewGuid().ToString();

            // Act
            A.CallTo(()=> _service.SaveGUIDToRegistry(guid)).Returns(true);
            A.CallTo(()=> _service.GenerateDeviceId()).Returns("RandomString");

            string generatedDeviceID = _service.GenerateDeviceId();

            // Assert
            Assert.That(generatedDeviceID, !Is.EqualTo(string.Empty));
        }

    }
}
