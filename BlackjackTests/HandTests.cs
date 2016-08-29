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
            _testObj = new Hand(_randomizer);
        }

        private Hand _testObj;
        private FakeRandomer _randomizer;

        [Test]
        public void GetHandScore()
        {
            _randomizer.AddValue(2);
            _testObj.Deal();
            Assert.That(_testObj.GetHandScore(), Is.EqualTo(4));
        }

        [Test]
        public void GetHandScore_HandlesAcesAsEleven()
        {
            _randomizer.AddValue(1);
            _randomizer.AddValue(2);
            _testObj.Deal();
            Assert.That(_testObj.GetHandScore(), Is.EqualTo(13));
        }

        [Test, Ignore("")]
        public void GetHandScore_HandlesAcesAsOne()
        {
            _randomizer = new FakeRandomer();
            _randomizer.AddValue(9);
            _randomizer.AddValue(9);
            _testObj = new Hand(_randomizer);
        }
    }
}