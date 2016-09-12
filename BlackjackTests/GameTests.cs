using Blackjack;
using NUnit.Framework;

namespace BlackjackTests
{
    [TestFixture]
    public class GameTests
    {
        [SetUp]
        public void Setup()
        {
            _fakeWriter = new FakeWriter();
            _randomer = new FakeRandomer();
            _yourHand = new Hand(_randomer);
            _fakeInput = new FakeInput();
        }

        private FakeRandomer _randomer;
        private Hand _yourHand;
        private FakeInput _fakeInput;
        private FakeWriter _fakeWriter;

        [Test]
        public void HittingMakesHandBigger()
        {
            _randomer.AddValue(2);

            _fakeInput.AddValue("h");
            _fakeInput.AddValue("s");
            new Game().HandlePlayerDraw(_fakeWriter, _fakeInput, _yourHand);

            Assert.That(_yourHand.GetHandScore(), Is.EqualTo(2));
        }

        [Test]
        public void HittingTwiceMakesHandEvenBigger()
        {
            _randomer.AddValue(2);
            _fakeInput.AddValue("h");
            _fakeInput.AddValue("h");
            _fakeInput.AddValue("s");
            new Game().HandlePlayerDraw(_fakeWriter, _fakeInput, _yourHand);

            Assert.That(_yourHand.GetHandScore(), Is.EqualTo(4));
        }

        [Test]
        public void PlayerBustBreaksLoop()
        {
            _randomer.AddValue(10);
            _fakeInput.AddValue("h");
            _fakeInput.AddValue("h");
            _fakeInput.AddValue("h");

            new Game().HandlePlayerDraw(_fakeWriter, _fakeInput, _yourHand);

            Assert.That(_yourHand.GetHandScore(), Is.EqualTo(30));
        }
    }
}