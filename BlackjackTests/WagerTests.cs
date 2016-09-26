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
            _testObj = new Wager(_fakeWriter);
        }

        private FakeWriter _fakeWriter;
        private Wager _testObj;

        [Test]
        public void GettingWagerFromUser()
        {
            _testObj.ReadWager("10");
            Assert.That(_testObj.LoseAmount(), Is.EqualTo(10));
            Assert.That(_testObj.WinAmount(), Is.EqualTo(15));
        }

        [Test]
        public void WagerDefaultsTo25()
        {
            _testObj.ReadWager("");

            Assert.That(_testObj.LoseAmount(), Is.EqualTo(25));
        }

        [Test]
        public void WagerMinAndMaxAreRespected()
        {
            _testObj.ReadWager("4");

            Assert.That(_testObj.LoseAmount(), Is.EqualTo(10));
            _testObj.ReadWager("16567801");

            Assert.That(_testObj.LoseAmount(), Is.EqualTo(100));
        }
    }
}