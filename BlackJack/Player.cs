using System;

namespace BlackJack;

public class Player(string name)
{
    private readonly Variables _variables = new Variables();

    public float Money { get; set; }

    private string Name { get; set; } = name;

    public List<Hand> Hands { get; set; } = new List<Hand>() { new Hand() };

    public bool Insurance { get; set; }

    public void DisplayHand()
    {
        Console.WriteLine($"Player: {Name}");
        foreach (var hand in Hands)
        {
            hand.DisplayHand();
            Console.WriteLine($"Hand Value: {hand.GetHandValue()}");
        }
    }

    public void AddCardToHand(Card card, int handIndex)
    {
        Hands[handIndex].AddCard(card);
    }

    public bool IsBusted(int handIndex)
    {
        return Hands[handIndex].IsBusted();
    }

    public void Hit(Card card, int handIndex)
    {
        Hands[handIndex].AddCard(card);
        if (IsBusted(handIndex))
        {
            Console.WriteLine("Busted!");
        }
    }

    public void Stand()
    {
        return;
    }

    public void DoubleDown(Card card, int handIndex)
    {
        if (Money < Hands[handIndex].Bet)
        {
            Console.WriteLine("Not enough money to double down");
            return;
        }

        Money -= Hands[handIndex].Bet;
        Hands[handIndex].Bet += Hands[handIndex].Bet;
        Hit(card, handIndex);
    }

    public void Split()
    {
        if (Money < Hands[0].Bet)
        {
            Console.WriteLine("Not enough money to split");
            return;
        }

        Money -= Hands[0].Bet;
        Hands.Add(new Hand());
        Hands[1].AddCard(Hands[0].Cards[1]);
        Hands[0].Cards.RemoveAt(1);
    }

    public void AddMoney(float amount)
    {
        Money += amount;
    }

    public void PlaceInsuranceBet()
    {
        Money -= Hands[0].Bet / 2;
        Insurance = true;
    }

    public void WinInsuranceBet()
    {
        Money += Hands[0].Bet;
    }

    public void LoseInsuranceBet()
    {
        Hands[0].Bet = 0;
    }
}