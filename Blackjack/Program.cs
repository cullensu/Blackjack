﻿using System;

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
            var money = 500;
            writer.WriteLine("Welcome to blackjack. You have $500. You win at $1000.");
            var game = new Game();
            var wager = new Wager(writer, input);
            while (money > 0)
            {
                wager.GetWager();

                var yourHand = DealYourHand(random, writer);
                var dealerHand = DealDealerHand(random, writer);

                game.HandlePlayerDraw(writer, input, yourHand);
                HandleDealerDraw(writer, dealerHand);

                money = DecideAndOutputWinner(writer, yourHand, dealerHand, money, wager);

                if (money >= 1000)
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

        private static int DecideAndOutputWinner(Writer writer, Hand yourHand, Hand dealerHand, int money, Wager wager)
        {
            var yourScore = yourHand.GetHandScore();
            var dealScore = dealerHand.GetHandScore();
            if ((yourScore < dealScore) || yourHand.Busted())
            {
                var loseAmount = wager.LoseAmount();
                money -= loseAmount;
                var loseMessage = yourHand.Busted() ? "You busted!" : "You lost!";
                writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. {loseMessage} You now have ${money} (-${loseAmount})");
            }
            else if (yourScore == dealScore)
            {
                writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. It's a push! You now have ${money} (+$0))");
            }
            else
            {
                var winAmount = wager.WinAmount();
                money += winAmount;
                writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. You won! You now have ${money} (+${winAmount}).");
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