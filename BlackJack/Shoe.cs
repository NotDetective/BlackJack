namespace BlackJack;

public class Shoe
{
    private readonly Variables _variables = new Variables();
    private List<Card> Cards { get; set; } = new List<Card>();

    public void AddDeck(Deck deck)
    {
        // check how many decks are in the shoe
        if (Cards.Count / _variables.CardsPerDeck >= _variables.MaxDecks)
        {
            Console.WriteLine("Max decks reached");
            return;
        }

        // add the deck to the shoe
        Cards.AddRange(deck.Cards);

        // clear the deck
        deck.Cards.Clear();
    }

    //TODO: Delete this before commit to main
    public void PrintShoe()
    {
        foreach (var card in Cards)
        {
            Console.WriteLine(card.GetCardName());
        }
    }

}