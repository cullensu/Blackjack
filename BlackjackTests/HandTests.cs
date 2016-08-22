using Blackjack;
using NUnit.Framework;

namespace BlackjackTests
{
    [TestFixture]
    public class HandTests
    {
        [SetUp]
        public void Setup()
        {
            _randomizer = new FakeRandomer();
            _randomizer.AddValue(2);
            _testObj = new Hand(_randomizer);
        }

        private Hand _testObj;
        private FakeRandomer _randomizer;

        [Test]
        public void GetHandScore()
        {
            Assert.That(_testObj.GetHandScore(), Is.EqualTo(4));
        }
    }
}