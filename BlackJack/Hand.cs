namespace BlackJack;

public class Hand
{

    private readonly Variables _variables = new Variables();
    public List<Card> Cards { get; set; } = new List<Card>();
    public float Bet { get; set; }

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public int GetHandValue()
    {
        return Cards.Sum(card => card.GetValue());
    }

    public void DisplayHand()
    {
        foreach (var card in Cards)
        {
            Console.WriteLine(card.GetCardName());
        }
    }

    public bool IsBlackJack()
    {
        return Cards.Count == 2 && GetHandValue() == _variables.BlackJackAmount;
    }

    public bool IsBusted()
    {
        return GetHandValue() > _variables.BustAmount;
    }

}