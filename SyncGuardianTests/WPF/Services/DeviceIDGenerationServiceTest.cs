using SyncGuardianWpf.Services;
using SyncGuardianWpf.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncGuardianTests.WPF.Services
{
    [TestFixture]
    public class DeviceIDGenerationServiceTest
    {
        private IDeviceIDGenerationService _service;

        //Global Arrange for DI
        [SetUp]
        public void Setup()
        {
            // Initialize your DI once
            // This could be setting up your IoC container and resolving the service
            _service = new DeviceIDGenerationService();
        }

        [Test]
        public void GenerateDeviceID_ReturnsNonEmptyString()
        {
            // Act
            string generatedDeviceID = _service.GenerateDeviceId();

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(generatedDeviceID));
        }

        [Test]
        public void GenerateDeviceID_ReturnsUniqueHash()
        {
            // Act
            string deviceIdOne = _service.GenerateDeviceId();
            string deviceIdTwo = _service.GenerateDeviceId();

            // Assert
            Assert.That(deviceIdTwo, Is.Not.EqualTo(deviceIdOne));
        }

        [Test]
        public void GenerateDeviceID_GetWindowsVersion()
        {
            // Act
            string windowsVersion = _service.GetWindowsVersion();

            // Assert
            Assert.That(windowsVersion == Environment.OSVersion.Version.Major.ToString(), Is.True);
        }

        [Test]
        public void GenerateDeviceID_GetGPUDetails()
        {
            // Act
            string gpuDetails = _service.GetGPUDetails();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(gpuDetails));
        }

        [Test]
        public void GenerateDeviceID_GetCPUDetails()
        {
            // Act
            string cpuDetails = _service.GetCPUDetails();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(cpuDetails));
        }

        [Test]
        public void GenerateDeviceID_ComputeHash()
        {
            // Arrange
            string randomGuid = Guid.NewGuid().ToString();

            // Act
            string hash = _service.ComputeHash(randomGuid);

            // Assert
            Assert.That(hash, Is.Not.EqualTo(randomGuid));
        }

    }
}
