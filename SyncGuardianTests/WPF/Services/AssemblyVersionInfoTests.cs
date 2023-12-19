using FakeItEasy;
using SyncGuardianWpf.Services.Interfaces;

namespace SyncGuardianTests.WPF.Services
{
    [TestFixture]
    public class AssemblyVersionInfoTests
    {
        private IAssemblyVersionInfo _assemblyVersionInfo;

        [SetUp]
        public void Setup()
        {
            _assemblyVersionInfo = A.Fake<IAssemblyVersionInfo>();
        }

        [Test]
        public void GetVersionInfo()
        {
            // Arrange
            A.CallTo(() => _assemblyVersionInfo.GetVersionInfo()).Returns("0.0.1");

            // Act
            var result = _assemblyVersionInfo.GetVersionInfo();

            // Assert
            Assert.That(result, !Is.EqualTo(string.Empty));
        }
    }
}
