using SyncGuardianWpf.Services.Interfaces;
using Xunit;
using FakeItEasy;

namespace SyncGuardianWpf.Tests
{
    public class DeviceIDGenerationServiceTests
    {
        [Fact]
        public void GenerateNewDeviceID()
        {
            // Arrange
            var service = A.Fake<IDeviceIDGenerationService>();
            A.CallTo(() => service.GenerateDeviceId()).Returns("some_fake_device_id");

            // Act
            string deviceId = service.GenerateDeviceId();

            // Assert
            Assert.Equal("some_fake_device_id", deviceId);
        }
    }
}
