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

            Assert.That(_testObj.GetWager(), Is.EqualTo(10));
        }

        [Test]
        public void WagerDefaultsTo25()
        {
            _fakeInput.AddValue("");

            Assert.That(_testObj.GetWager(), Is.EqualTo(25));
        }

        [Test]
        public void WagerMinAndMaxAreRespected()
        {
            _fakeInput.AddValue("4");
            Assert.That(_testObj.GetWager(), Is.EqualTo(10));
            _fakeInput.AddValue("16567801");
            Assert.That(_testObj.GetWager(), Is.EqualTo(100));
        }
    }
}