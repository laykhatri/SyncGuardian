using FakeItEasy;
using Serilog;
using SG.Server.Services.Interfaces;
using System.Text.Json;

namespace SG.Tests.Server.Services
{
    [TestFixture]
    public class QRCodeGenerationServiceTests
    {
        private IQRCodeGenerationService _qrCodeGenerationService;
        private ILogger _logger;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _qrCodeGenerationService = A.Fake<IQRCodeGenerationService>();
        }

        [Test]
        public void Validate_QRCodeGenerated()
        {
            var testData = new { column1 = 1, column2 = 2 };
            var qrCode = _qrCodeGenerationService.GenerateQRCodeImage(JsonSerializer.Serialize(testData));

            Assert.That(qrCode, Is.Not.Null);
        }

        [Test]
        public void Validate_QRCodeNotBeingGenerated()
        {
            var testData = "helloworld";
            var qrCode = _qrCodeGenerationService.GenerateQRCodeImage(JsonSerializer.Serialize(testData));

            Assert.That(qrCode, Is.Not.Null);
            Assert.That(qrCode.Length, Is.GreaterThan(0));
        }
    }
}
