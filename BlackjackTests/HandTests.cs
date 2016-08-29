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
        public void DrawCard_UsesRandomer()
        {
            _randomizer.AddValue(3);
            Assert.That(_testObj.DrawCard(), Is.EqualTo(3));
        }

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

        [Test]
        public void GetHandScore_HandlesNCards()
        {
            _randomizer.AddValue(2);
            _testObj.Deal();
            _randomizer.AddValue(5);
            _testObj.DrawCard();
            _testObj.DrawCard();
            Assert.That(_testObj.GetHandScore(), Is.EqualTo(2 + 2 + 5 + 2));
        }
    }
}