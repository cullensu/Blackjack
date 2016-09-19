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
        public virtual string NextKey()
        {
            return Console.ReadKey().KeyChar.ToString();
        }

        public virtual string NextInput()
        {
            return Console.ReadLine();
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
            var bank = new Bank(500, 1000, writer);
            bank.Introduce();
            var game = new Game();
            var wager = new Wager(writer, input);
            while (bank.HasMoney())
            {
                wager.GetWager();

                var yourHand = DealYourHand(random, writer);
                var dealerHand = DealDealerHand(random, writer);

                game.HandlePlayerDraw(writer, input, yourHand);
                HandleDealerDraw(writer, dealerHand);

                var diffMoney = DecideAndOutputWinner(writer, yourHand, dealerHand, wager);
                bank.Settle(diffMoney);

                writer.WriteLine();

                if (bank.HasEnoughMoney())
                {
                    writer.WriteLine("You win!");
                    input.NextKey();
                    return;
                }
            }

            writer.WriteLine("You lose.");
            input.NextKey();
        }

        private static Hand DealDealerHand(Randomer random, Writer writer)
        {
            var dealerHand = GetNewHand(random);
            writer.WriteLine(
                $"The dealer is showing {dealerHand.GetCardName(0)}.");
            return dealerHand;
        }

        private static Hand DealYourHand(Randomer random, Writer writer)
        {
            var yourHand = GetNewHand(random);
            writer.WriteLine(
                $"Your cards are {yourHand.GetCardName(0)} and {yourHand.GetCardName(1)}");
            return yourHand;
        }

        private static void HandleDealerDraw(Writer writer, Hand dealerHand)
        {
            writer.WriteLine(Environment.NewLine +
                             $"The dealer flips their other card over. It's {dealerHand.GetCardName(1)}.");
            if (dealerHand.GetHandScore() < 17)
            {
                var newCard = dealerHand.Draw();
                writer.WriteLine($"The dealer adds another card to their hand. It's {Card.GetNameOf(newCard)}.");
            }
        }

        private static int DecideAndOutputWinner(Writer writer, Hand yourHand, Hand dealerHand, Wager wager)
        {
            var diffMoney = 0;
            var yourScore = yourHand.GetHandScore();
            var dealScore = dealerHand.GetHandScore();
            if ((yourScore < dealScore) || yourHand.Busted())
            {
                var loseAmount = wager.LoseAmount();
                diffMoney -= loseAmount;
                var loseMessage = yourHand.Busted() ? "You busted!" : "You lost!";
                writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. {loseMessage}.");
            }
            else if (yourScore == dealScore)
            {
                writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. It's a push!");
            }
            else
            {
                var winAmount = wager.WinAmount();
                diffMoney += winAmount;
                writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. You won!");
            }
            return diffMoney;
        }

        private static Hand GetNewHand(Randomer random)
        {
            var hand = new Hand(random);
            hand.Deal();
            return hand;
        }
    }

    public class Bank
    {
        private int _money;
        private readonly int _goalMoney;
        private readonly Writer _writer;

        public Bank(int startingMoney, int goalMoney, Writer writer)
        {
            _money = startingMoney;
            _goalMoney = goalMoney;
            _writer = writer;
        }

        public bool HasMoney()
        {
            return _money > 0;
        }

        public void Settle(int diffMoney)
        {
            _money += diffMoney;
            _writer.WriteLine($"You now have ${_money} (${diffMoney})");
        }

        public bool HasEnoughMoney()
        {
            return _money >= 1000;
        }

        public void Introduce()
        {
            _writer.WriteLine($"Welcome to blackjack. You have ${_money}. You win at ${_goalMoney}.");
        }
    }
}