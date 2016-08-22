using System.Collections.Generic;
using System.Linq;
using Blackjack;
using NUnit.Framework;

namespace BlackjackTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void YouCanLose()
        {
            var program = new Program();
            var fakeWriter = new FakeWriter();

            program.Game(new FakeRandomer(), fakeWriter, new FakeInput());
            Assert.That(fakeWriter.output[0],
                Is.EqualTo("Welcome to blackjack. You have $500. Each hand costs $25. You win at $1000."));
            Assert.That(fakeWriter.output.Last(), Is.EqualTo("You lose."));
        }
    }

    internal class FakeWriter : Writer
    {
        public List<string> output = new List<string>();

        public override void WriteLine(string value = "")
        {
            output.Add(value);
        }
    }

    internal class FakeInput : Input
    {
        public override string NextInput()
        {
            return "s";
        }
    }

    internal class FakeRandomer : Randomer
    {
        public override int Next(int min, int max)
        {
            return 2;
        }
    }
}