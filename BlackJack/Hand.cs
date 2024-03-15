namespace BlackJack;

public class Hand
{

public List<Card> Cards { get; set; } = new List<Card>();

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public int GetHandValue()
    {
        int handValue = 0;
        foreach (var card in Cards)
        {
            handValue += card.GetValue();
        }
        return handValue;
    }

    public void DisplayHand()
    {
        foreach (var card in Cards)
        {
            Console.WriteLine(card.GetCardName());
        }
    }

}