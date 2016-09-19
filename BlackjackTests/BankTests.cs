using Blackjack;
using NUnit.Framework;

namespace BlackjackTests
{
    [TestFixture]
    public class BankTests
    {
        private Bank _testObj;
        private FakeWriter _fakeWriter;

        [SetUp]
        public void Setup()
        {
            _fakeWriter = new FakeWriter();
            _testObj = new Bank(100, 200, _fakeWriter);
        }

        [Test]
        public void HasMoneyStarting()
        {
            Assert.True(_testObj.HasMoney());
        }

        [Test]
        public void HasEnoughMoneyStarting()
        {
            Assert.False(_testObj.HasEnoughMoney());
        }

        [Test]
        public void OutOfMoney()
        {
            _testObj.Settle(-100);
            Assert.False(_testObj.HasMoney());
        }

        [Test]
        public void WinnerWinner()
        {
            _testObj.Settle(100);
            Assert.True(_testObj.HasEnoughMoney());
        }
    }
}