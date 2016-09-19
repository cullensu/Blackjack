using Blackjack;
using NUnit.Framework;

namespace BlackjackTests
{
    [TestFixture]
    public class DeckTests
    {
        private FakeWriter _fakeWriter;
        private FakeRandomer _randomer;
        private Deck _testObj;

        [SetUp]
        public void Setup()
        {
            _fakeWriter = new FakeWriter();
            _randomer = new FakeRandomer();
            _testObj = new Deck(_randomer, _fakeWriter);
        }

        [Test]
        public void DealPlayerHand()
        {
            _randomer.AddValue(4);
            var dealHand = _testObj.DealDealerHand();
            Assert.That(dealHand.GetHandScore(), Is.EqualTo(8));
        }

        [Test]
        public void DealDealerHander()
        {
            _randomer.AddValue(5);
            var dealHand = _testObj.DealDealerHand();
            Assert.That(dealHand.GetHandScore(), Is.EqualTo(10));
        }
    }
}