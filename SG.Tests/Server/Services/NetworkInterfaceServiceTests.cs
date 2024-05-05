using FakeItEasy;
using Serilog;
using SG.Server.Services.Interfaces;

namespace SG.Tests.Server.Services
{
    [TestFixture]
    public class NetworkInterfaceServiceTests
    {
        private INetworkInterfaceService _service;
        private ILogger _logger;

        //Global Arrange for DI
        [SetUp]
        public void Setup()
        {
            // Initialize your DI once
            // This could be setting up your IoC container and resolving the service
            _logger = A.Fake<ILogger>();
            _service = A.Fake<INetworkInterfaceService>();
        }

        [Test]
        [TestCase(false, false)]
        [TestCase(true, true)]
        public void IsNetworkConnected_False(bool network, bool output)
        {
            // Act
            A.CallTo(() => _service.IsNetworkConnected()).Returns(network);

            var networkConnected = _service.IsNetworkConnected();

            // Assert
            Assert.That(networkConnected, Is.EqualTo(output));
        }

        [Test]
        public void GetLocalIpAddress()
        {
            // Arrange
            var localHost = "127.0.0.1";

            // Act
            A.CallTo(() => _service.GetLocalIpAddress()).Returns(localHost);

            var ipAddress = _service.GetLocalIpAddress();

            // Assert
            Assert.That(ipAddress, Is.EqualTo(localHost));
        }
    }
}
