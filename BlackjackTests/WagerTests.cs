using Blackjack;
using NUnit.Framework;

namespace BlackjackTests
{
    [TestFixture]
    public class WagerTests
    {
        [SetUp]
        public void Setup()
        {
            _fakeWriter = new FakeWriter();
            _fakeInput = new FakeInput();
            _testObj = new Wager(_fakeWriter, _fakeInput);
        }

        private FakeWriter _fakeWriter;
        private Wager _testObj;
        private FakeInput _fakeInput;

        [Test]
        public void GettingWagerFromUser()
        {
            _fakeInput.AddValue("10");
            _testObj.GetWager();
            Assert.That(_testObj.LoseAmount(), Is.EqualTo(10));
            Assert.That(_testObj.WinAmount(), Is.EqualTo(15));
        }

        [Test]
        public void WagerDefaultsTo25()
        {
            _fakeInput.AddValue("");
            _testObj.GetWager();

            Assert.That(_testObj.LoseAmount(), Is.EqualTo(25));
        }

        [Test]
        public void WagerMinAndMaxAreRespected()
        {
            _fakeInput.AddValue("4");
            _testObj.GetWager();

            Assert.That(_testObj.LoseAmount(), Is.EqualTo(10));
            _fakeInput.AddValue("16567801");
            _testObj.GetWager();

            Assert.That(_testObj.LoseAmount(), Is.EqualTo(100));
        }
    }
}