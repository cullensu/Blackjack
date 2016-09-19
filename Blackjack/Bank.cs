using System;

namespace Blackjack
{
    public class Bank
    {
        private readonly int _goalMoney;
        private readonly Writer _writer;
        private int _money;

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
            var preceder = "$";
            if (diffMoney > 0)
                preceder = "+$";
            else if (diffMoney < 0)
            {
                preceder = "-$";
                diffMoney = Math.Abs(diffMoney);
            }

            _writer.WriteLine($"You now have ${_money} ({preceder}{diffMoney})");
        }

        public bool HasEnoughMoney()
        {
            return _money >= _goalMoney;
        }

        public void Introduce()
        {
            _writer.WriteLine($"Welcome to blackjack. You have ${_money}. You win at ${_goalMoney}.");
        }
    }
}