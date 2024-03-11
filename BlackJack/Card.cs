namespace BlackJack;

public class Card
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
        Ace = 11,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 10,
        Queen = 10,
        King = 10
    }


    public Suit CardSuit { get; set; }

    public Rank CardRank { get; set; }

    public Card(Suit suit, Rank rank)
    {
        CardSuit = suit;
        CardRank = rank;
    }

    public int GetValue()
    {
        return (int)CardRank;
    }

    public string GetCardName()
    {
        return $"{CardRank} of {CardSuit}";
    }

}