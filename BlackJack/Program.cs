namespace BlackJack;

class Program
{
    static void Main(string[] args)
    {
        Card card = new Card(Card.Suit.Hearts, Card.Rank.Ace);

        Console.WriteLine(card.GetCardName());

        Console.WriteLine(card.GetValue());

        Console.WriteLine(card.CardSuit);

        Console.WriteLine(card.CardRank);
    }
}