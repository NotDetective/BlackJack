namespace BlackJack;

public class Variables
{
    public int BustAmount { get; set; } = 21;

    public int BlackJackAmount { get; set; } = 21;

    public int DealerStandAmount { get; set; } = 17;

    public int StartingHandSize { get; set; } = 2;

    public int MaxHandSize { get; set; } = 5;

    public int MaxPlayers { get; set; } = 4;

    public int MinPlayers { get; set; } = 1;

    public int MaxDecks { get; set; } = 8;

    public int MinDecks { get; set; } = 1;

    public int MinBet { get; set; } = 10;
}