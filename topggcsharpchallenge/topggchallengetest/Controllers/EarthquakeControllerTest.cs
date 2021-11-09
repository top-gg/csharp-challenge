using NUnit.Framework;
using topggcsharpchallenge.Controllers;

namespace topggcsharpchallengetest.Controllers
{
    [TestFixture]
    public class EarthquakeControllerTest
    {
        EearthquakeController sut;

        [SetUp]
        public void SetUp()
        {
            sut = new EearthquakeController();
        }

        [Test]
        public void GetShouldBeSuccessfull()
        {
            sut.Get();
        }
    }
}
