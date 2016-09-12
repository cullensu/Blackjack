namespace Blackjack
{
    public class Game
    {
        public void HandlePlayerDraw(Writer writer, Input input,
            Hand yourHand)
        {
            var inputString = "";

            do
            {
                inputString = "";
                while ((inputString != "h") && (inputString != "s"))
                {
                    writer.WriteLine("Do you (h)it or (s)tay?");
                    inputString = input.NextInput();
                    writer.WriteLine();
                }

                if (inputString == "h")
                {
                    var newCard = yourHand.Draw();
                    writer.WriteLine(
                        $"The dealer slides another card to you. It's {Card.GetNameOf(newCard)}. Your total is: {yourHand.GetHandScore()}");
                }
            } while ((inputString != "s") && !yourHand.Busted());
        }
    }
}