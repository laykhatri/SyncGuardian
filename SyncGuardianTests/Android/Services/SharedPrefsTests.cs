using FakeItEasy;
using SyncGuardianMobile.Services;
using SyncGuardianMobile.Services.Interface;
using System.Collections;

namespace SyncGuardianTests.Android.Services
{
    [TestFixture]
    public class SharedPrefsTests
    {
        private ISharedPrefs _sharedPref;

        [SetUp]
        public void Setup()
        {
            _sharedPref = A.Fake<ISharedPrefs>();
        }

        [Test]
        public void SharedPref_GetValueTest()
        {
            var resultBool = _sharedPref.GetValue<bool>("FakeSharedPref");
            Assert.That(resultBool, Is.EqualTo(false));

            var resultInt = _sharedPref.GetValue<int>("FakeSharedPref");
            Assert.That(resultInt, Is.EqualTo(0));

            var resultString = _sharedPref.GetValue<string>("FakeSharedPref");
            Assert.That(resultString, Is.EqualTo(""));

            var resultLong = _sharedPref.GetValue<long>("FakeSharedPref");
            Assert.That(resultLong, Is.EqualTo(0L));

            var resultDecimal = _sharedPref.GetValue<decimal>("FakeSharedPref");
            Assert.That(resultDecimal, Is.EqualTo(0));
        }
    }
}
