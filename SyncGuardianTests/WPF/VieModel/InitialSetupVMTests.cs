using FakeItEasy;
using Serilog;
using SyncGuardianWpf.Services.Interfaces;
using SyncGuardianWpf.ViewModels;

namespace SyncGuardianTests.WPF.VieModel
{
    public class InitialSetupVMTests
    {
        private InitialSetupViewModel _vm;
        private ILogger _logger;
        private INetworkInterfaceService _networkService;
        private IDeviceIDGenerationService _deviceIDGenerationService;
        private IQRCodeGenerationService _qrCodeGenerationService;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger>();
            _networkService = A.Fake<INetworkInterfaceService>();
            _deviceIDGenerationService = A.Fake<IDeviceIDGenerationService>();
            _qrCodeGenerationService = A.Fake<IQRCodeGenerationService>();
            _vm = new InitialSetupViewModel(_logger, _networkService, _deviceIDGenerationService, _qrCodeGenerationService);
        }

        [Test]
        public void Test_InitialSetup_SetupGridNotVisible()
        {
            // Arrange
            A.CallTo(() =>  _networkService.IsNetworkConnected()).Returns(false);

            // Act
            _vm.InitialSetup();

            // Asset
            Assert.IsFalse(_vm.SetupGridVisible);
        }

        [Test]
        public void Test_InitialSetup_SetupGridVisible()
        {
            // Arrange
            A.CallTo(() => _networkService.IsNetworkConnected()).Returns(true);

            // Act
            _vm.InitialSetup();

            // Asset
            Assert.IsTrue(_vm.SetupGridVisible);
        }
    }
}
