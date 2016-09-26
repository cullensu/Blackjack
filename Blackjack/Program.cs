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
            var input = new Input();
            program.Game(new Randomer(), new Writer(), input);
            input.NextKey();
        }

        public void Game(Randomer random, Writer writer, Input input)
        {
            var wallet = new Wallet(500, 1000, writer);
            wallet.Introduce();
            var game = new Game(writer, input, random);
            var wager = new Wager(writer);
            while (wallet.HasMoney())
            {
                game.PlayHand(wager, wallet);

                writer.WriteLine();

                if (wallet.HasEnoughMoney())
                {
                    writer.WriteLine("You win!");
                    return;
                }
            }

            writer.WriteLine("You lose.");
        }
    }
}