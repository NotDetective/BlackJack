using System;

namespace BlackJack;

public class Player(string name, Hand hand)
{
    private readonly Variables _variables = new Variables();

    public int Score { get; set; }
    private string Name { get; set; } = name;

    private Hand PlayerHand { get; set; } = hand;

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
        return PlayerHand.GetHandValue() > _variables.BustAmount;
    }

    public void Hit()
    {
        
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