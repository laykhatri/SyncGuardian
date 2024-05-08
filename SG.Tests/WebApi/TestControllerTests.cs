using SG.WebAPI.Controllers;

namespace SG.Tests.WebApi
{
    [TestFixture]
    public class TestControllerTests
    {
        private TestController controller;

        [SetUp]
        public void Setup()
        {
            controller = new TestController();
        }

        [Test]
        public void Index_Method()
        {
            // Act
            var result = controller.Index();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<string>());
            Assert.That(result, Does.Contain("Welcome to SyncGuardian WebApi"));
        }
    }
}
