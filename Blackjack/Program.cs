using System;

namespace Blackjack
{
    public class Writer
    {
        public virtual void WriteLine(string value = "")
        {
            Console.WriteLine(value);
        }
    }

    public class Input
    {
        public virtual string NextInput()
        {
            return Console.ReadKey().KeyChar.ToString();
        }
    }

    public class Randomer
    {
        private readonly Random _random = new Random();

        public virtual int Next(int min, int max)
        {
            return _random.Next(min, max);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var program = new Program();
            program.Game(new Randomer(), new Writer(), new Input());
        }

        public void Game(Randomer random, Writer writer, Input input)
        {
            var money = 500;
            writer.WriteLine("Welcome to blackjack. You have $500. Each hand costs $25. You win at $1000.");

            while (money > 0)
            {
                var yourHand = GetNewHand(random);

                writer.WriteLine(
                    $"Your cards are {yourHand.GetCardName(0)} and {yourHand.GetCardName(1)}");

                var dealerHand = GetNewHand(random);
                writer.WriteLine(
                    $"The dealer is showing a {dealerHand.GetCardName(0)}. Do you (h)it or (s)tay?");

                var inputString = input.NextInput();
                writer.WriteLine();
                while (inputString != "h" && inputString != "s")
                {
                    writer.WriteLine("Do you (h)it or (s)tay?");
                    inputString = input.NextInput();
                    writer.WriteLine();
                }

                if (inputString == "s")
                {
                    writer.WriteLine(Environment.NewLine +
                                     $"The dealer flips their other card over. It's a {dealerHand.GetCardName(1)}.");
                }
                var newCard = 0;
                if (inputString == "h")
                {
                    newCard = yourHand.Draw();
                    var n = "";
                    if (newCard == 1)
                    {
                        n = "n";
                    }
                    writer.WriteLine($"The dealer slides another card to you. It's a{n} {Hand.GetNameOf(newCard)}.");
                }

                if (dealerHand.GetHandScore() < 17)
                {
                    newCard = dealerHand.Draw();
                    var n = "";
                    if (newCard == 1)
                    {
                        n = "n";
                    }
                    writer.WriteLine($"The dealer adds another card to their hand. It's a{n} {Hand.GetNameOf(newCard)}.");
                }

                money = DecideAndOutputWinner(writer, yourHand, dealerHand, money);

                if (money >= 1000)
                {
                    writer.WriteLine("You win!");
                    input.NextInput();
                    return;
                }
            }

            writer.WriteLine("You lose.");
            input.NextInput();
        }

        private static int DecideAndOutputWinner(Writer writer, Hand yourHand, Hand dealerHand, int money)
        {
            var yourScore = yourHand.GetHandScore();
            var dealScore = dealerHand.GetHandScore();
            if (yourScore < dealScore || yourScore > 21)
            {
                money -= 25;
                var loseMessage = yourScore > 21 ? "You busted!" : "You lost!";
                writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. {loseMessage} You now have ${money} (-$25)");
            }
            else if (yourScore == dealScore)
            {
                writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. It's a push! You now have ${money} (+$0))");
            }
            else
            {
                money += 25;
                writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. You won! You now have ${money} (+$25).");
            }
            writer.WriteLine();
            return money;
        }

        private static Hand GetNewHand(Randomer random)
        {
            var hand = new Hand(random);
            hand.Deal();
            return hand;
        }
    }
}