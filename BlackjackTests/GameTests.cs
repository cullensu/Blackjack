using Blackjack;
using NUnit.Framework;

namespace BlackjackTests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void HittingMakesHandBigger()
        {
            var randomer = new FakeRandomer();
            randomer.AddValue(2);
            var hand = new Hand(randomer);

            var fakeInput = new FakeInput();
            fakeInput.AddValue("h");
            new Game().HandlePlayerDraw(new FakeWriter(), fakeInput, hand);

            Assert.That(hand.GetHandScore(), Is.EqualTo(2));
        }
    }
}