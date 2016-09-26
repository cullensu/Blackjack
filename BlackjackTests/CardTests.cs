using Blackjack;
using NUnit.Framework;

namespace BlackjackTests
{
    [TestFixture]
    public class CardsTests
    {
        [Test]
        public void GetNamesFancyCards()
        {
            Assert.That(Card.GetNameOf(1), Is.EqualTo("an Ace"));
            Assert.That(Card.GetNameOf(11), Is.EqualTo("a Jack"));
            Assert.That(Card.GetNameOf(12), Is.EqualTo("a Queen"));
            Assert.That(Card.GetNameOf(13), Is.EqualTo("a King"));
        }

        [Test]
        public void GetNamesOfNumberCards()
        {
            Assert.That(Card.GetNameOf(2), Is.EqualTo("a 2"));
        }
    }
}