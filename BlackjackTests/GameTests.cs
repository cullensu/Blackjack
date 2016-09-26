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
            _testObj = new Game(_fakeWriter, _fakeInput, _randomer);
        }

        private FakeRandomer _randomer;
        private Hand _yourHand;
        private FakeInput _fakeInput;
        private FakeWriter _fakeWriter;
        private Game _testObj;

        [Test]
        public void HittingMakesHandBigger()
        {
            _randomer.AddValue(2);

            _fakeInput.AddValue("h");
            _fakeInput.AddValue("s");
            _testObj.HandlePlayerDraw(_yourHand);

            Assert.That(_yourHand.GetHandScore(), Is.EqualTo(2));
        }

        [Test]
        public void HittingTwiceMakesHandEvenBigger()
        {
            _randomer.AddValue(2);
            _fakeInput.AddValue("h");
            _fakeInput.AddValue("h");
            _fakeInput.AddValue("s");
            _testObj.HandlePlayerDraw(_yourHand);

            Assert.That(_yourHand.GetHandScore(), Is.EqualTo(4));
        }

        [Test]
        public void PlayBigHandLoose()
        {
            var wager = new Wager(_fakeWriter);
            var bank = new Bank(100, 200, _fakeWriter);
            _fakeInput.AddValue("100");
            _fakeInput.AddValue("s");
            _randomer.AddValue(7);
            _randomer.AddValue(10);
            _randomer.AddValue(10);
            _randomer.AddValue(8);

            _testObj.PlayHand(wager, bank);

            Assert.False(bank.HasEnoughMoney());
        }

        [Test]
        public void PlayBigHandWin()
        {
            var wager = new Wager(_fakeWriter);
            var bank = new Bank(100, 200, _fakeWriter);
            _fakeInput.AddValue("100");
            _fakeInput.AddValue("s");
            _randomer.AddValue(10);
            _randomer.AddValue(10);
            _randomer.AddValue(10);
            _randomer.AddValue(8);

            _testObj.PlayHand(wager, bank);

            Assert.True(bank.HasEnoughMoney());
        }

        [Test]
        public void PlayerBustBreaksLoop()
        {
            _randomer.AddValue(10);
            _fakeInput.AddValue("h");
            _fakeInput.AddValue("h");
            _fakeInput.AddValue("h");

            _testObj.HandlePlayerDraw(_yourHand);

            Assert.That(_yourHand.GetHandScore(), Is.EqualTo(30));
        }
    }
}