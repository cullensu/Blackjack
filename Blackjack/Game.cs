﻿using System;

namespace Blackjack
{
    public class Game
    {
        private readonly Writer _writer;
        private readonly Input _input;
        private readonly Randomer _random;

        public Game(Writer writer, Input input, Randomer random)
        {
            _writer = writer;
            _input = input;
            _random = random;
        }

        public void HandlePlayerDraw(Hand yourHand)
        {
            var inputString = "";

            do
            {
                inputString = "";
                while ((inputString != "h") && (inputString != "s"))
                {
                    _writer.WriteLine("Do you (h)it or (s)tay?");
                    inputString = _input.NextKey();
                    _writer.WriteLine();
                }

                if (inputString == "h")
                {
                    var newCard = yourHand.Draw();
                    _writer.WriteLine(
                        $"The dealer slides another card to you. It's {Card.GetNameOf(newCard)}. Your total is: {yourHand.GetHandScore()}");
                }
            } while ((inputString != "s") && !yourHand.Busted());
        }

        public void PlayHand(Wager wager, Bank bank)
        {
            wager.GetWager();

            var yourHand = DealYourHand();
            var dealerHand = DealDealerHand();

            HandlePlayerDraw(yourHand);
            HandleDealerDraw(dealerHand);

            var diffMoney = DecideWinner(yourHand, dealerHand, wager);
            bank.Settle(diffMoney);
        }

        private Hand DealDealerHand()
        {
            var dealerHand = GetNewHand();
            _writer.WriteLine(
                $"The dealer is showing {dealerHand.GetCardName(0)}.");
            return dealerHand;
        }

        private Hand DealYourHand()
        {
            var yourHand = GetNewHand();
            _writer.WriteLine(
                $"Your cards are {yourHand.GetCardName(0)} and {yourHand.GetCardName(1)}");
            return yourHand;
        }

        private void HandleDealerDraw(Hand dealerHand)
        {
            _writer.WriteLine(Environment.NewLine +
                             $"The dealer flips their other card over. It's {dealerHand.GetCardName(1)}.");
            if (dealerHand.GetHandScore() < 17)
            {
                var newCard = dealerHand.Draw();
                _writer.WriteLine($"The dealer adds another card to their hand. It's {Card.GetNameOf(newCard)}.");
            }
        }

        private int DecideWinner(Hand yourHand, Hand dealerHand, Wager wager)
        {
            var diffMoney = 0;
            var yourScore = yourHand.GetHandScore();
            var dealScore = dealerHand.GetHandScore();
            if ((yourScore < dealScore) || yourHand.Busted())
            {
                var loseAmount = wager.LoseAmount();
                diffMoney -= loseAmount;
                var loseMessage = yourHand.Busted() ? "You busted!" : "You lost!";
                _writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. {loseMessage}.");
            }
            else if (yourScore == dealScore)
            {
                _writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. It's a push!");
            }
            else
            {
                var winAmount = wager.WinAmount();
                diffMoney += winAmount;
                _writer.WriteLine(
                    $"You had {yourScore} and dealer had {dealScore}. You won!");
            }
            return diffMoney;
        }

        private Hand GetNewHand()
        {
            var hand = new Hand(_random);
            hand.Deal();
            return hand;
        }
    }
}