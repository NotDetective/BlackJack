namespace BlackJack;

public class Deck
{
    private List<Card> Cards { get; set; }

    public Deck()
    {
        Cards = new List<Card>();
        foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
        {
            foreach (Card.Rank rank in Enum.GetValues(typeof(Card.Rank)))
            {
                Cards.Add(new Card(suit, rank));
            }
        }
    }

    public void Shuffle()
    {
        Random rng = new Random();

        foreach (var card in Cards)
        {
            int number = Cards.IndexOf(card);
            int randomIndex = rng.Next(number + 1);
            (Cards[randomIndex], Cards[number]) = (Cards[number], Cards[randomIndex]);
        }
    }

    public Card? DrawCard()
    {
        if (IsEmpty())
        {
            Card card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
        return null;
    }

    public void PrintDeck()
    {
        foreach (Card card in Cards)
        {
            Console.WriteLine(card.GetCardName());
        }
    }

    private bool IsEmpty()
    {
        return Cards.Count == 0;
    }
}