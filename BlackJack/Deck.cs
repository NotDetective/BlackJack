using System;
using System.Collections.Generic;

namespace BlackJack;

public class Deck
{
    public List<Card> Cards { get; set; } = new List<Card>();

    public Deck()
    {
        foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
        {
            foreach (Card.Rank rank in Enum.GetValues(typeof(Card.Rank)))
            {
                Cards.Add(new Card(suit, rank));
            }
        }
    }
}