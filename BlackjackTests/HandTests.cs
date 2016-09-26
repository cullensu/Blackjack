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
            _testObj = new Hand();
        }

        private Hand _testObj;
        private FakeRandomer _randomizer;

        [Test]
        public void Busted_ReturnsTrueIfTotalMoreThanThreshold()
        {
            _testObj.AddCard(9);
            _testObj.AddCard(9);
            Assert.That(_testObj.Busted(), Is.False);
            _testObj.AddCard(9);
            Assert.That(_testObj.Busted(), Is.True);
        }

        [Test]
        public void GetHandScore()
        {
            _testObj.AddCard(2);
            _testObj.AddCard(2);
            Assert.That(_testObj.GetHandScore(), Is.EqualTo(4));
        }

        [Test]
        public void GetHandScore_HandlesAcesAsEleven()
        {
            _testObj.AddCard(1);
            _testObj.AddCard(2);
            Assert.That(_testObj.GetHandScore(), Is.EqualTo(13));
        }

        [Test]
        public void GetHandScore_HandlesAcesAsOne()
        {
            _testObj.AddCard(9);
            _testObj.AddCard(9);
            _testObj.AddCard(1);
            Assert.That(_testObj.GetHandScore(), Is.EqualTo(19));
        }

        [Test]
        public void GetHandScore_HandlesAcesAsOneOrElevenInSameHand()
        {
            _testObj.AddCard(1);
            _testObj.AddCard(1);
            Assert.That(_testObj.GetHandScore(), Is.EqualTo(12));
        }

        [Test]
        public void GetHandScore_HandlesNCards()
        {
            _testObj.AddCard(2);
            _testObj.AddCard(2);
            _testObj.AddCard(2);
            _testObj.AddCard(5);
            Assert.That(_testObj.GetHandScore(), Is.EqualTo(2 + 2 + 5 + 2));
        }
    }
}