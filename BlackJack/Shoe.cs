namespace BlackJack;

public class Shoe
{
    private readonly Variables _variables = new Variables();
    private List<Card> Cards { get; set; } = new List<Card>();
}