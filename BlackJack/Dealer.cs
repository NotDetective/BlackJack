namespace BlackJack;

public class Dealer(string name) : Player($"Dealer: {name}")
{
    private int Score { get; set; }

    public Shoe Shoe { get; set; } = new Shoe();

    public void AddScore(int score)
    {
        Score += score;
    }

    public void SubtractScore(int score)
    {
        Score -= score;
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Dealer Score: {Score}");
    }

    public Player GetPlayer()
    {
        return this;
    }

    public void ShuffleShoe()
    {
        Shoe.Shuffle();
    }

    public void DealCard(Player player, int handIndex)
    {
        player.AddCardToHand(Shoe.DrawCard(), handIndex);
    }
}