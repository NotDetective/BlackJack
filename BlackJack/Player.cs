namespace BlackJack;

public class Player
{

    public int Score { get; set; }
    public string Name { get; set; }

    public Hand PlayerHand { get; set; } = new Hand();

    public void DisplayHand()
    {
        Console.WriteLine($"Player: {Name}");
        PlayerHand.DisplayHand();
        Console.WriteLine($"Hand Value: {PlayerHand.GetHandValue()}");
    }

    public void AddCardToHand(Card card)
    {
        PlayerHand.AddCard(card);
    }

    public bool IsBusted()
    {
        return PlayerHand.GetHandValue() > 21;
    }

    public void Hit()
    {
        //
    }

    public void Stand()
    {
        //
    }

    public void DoubleDown()
    {
        //
    }

    public void Split()
    {
        //
    }

}