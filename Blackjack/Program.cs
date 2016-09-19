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
            var game = new Game(writer, input, random);
            var wager = new Wager(writer, input);
            while (bank.HasMoney())
            {
                game.PlayHand(wager, bank);

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
    }
}