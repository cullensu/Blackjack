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
        public void WagerDefaultsTo25()
        {
            Assert.That(_testObj.GetWager(), Is.EqualTo(25));
        }
    }
}