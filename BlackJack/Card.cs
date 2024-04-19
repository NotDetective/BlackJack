namespace BlackJack;

public class Card(Card.Suit suit, Card.Rank rank)
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum Rank
    {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13
    }


    private Suit CardSuit { get; set; } = suit;

    private Rank CardRank { get; set; } = rank;

    public int GetValue()
    {
        return (int)CardRank > 10 ? 10 : (int)CardRank;
    }

    public string GetCardName()
    {
        return $"{CardRank} of {CardSuit}";
    }

}