using System.Diagnostics;

namespace BlackJack;

public class Game
{
    readonly Variables _variables = new Variables();

    public Dealer Dealer { get; set; }

    private List<Player> Players { get; set; } = new List<Player>();

    public void StartGame()
    {
        SetUpGame();

    }

    private void SetUpGame()
    {
        SetUpDealer();
        SetUpPlayers();
        // start set up game
        // add decks to shoe
        // shuffle shoe
    }

    private void GameLoop()
    {
        // deal cards to players and dealer
        // check for blackjack
        // player turn
        // dealer turn
        // check for winner
        // start over or end game
    }

    private void SetUpDealer()
    {
        Console.WriteLine("Enter the dealer's name:");
        string dealerName = Console.ReadLine()!.Trim(); // Trim to remove leading and trailing whitespace

        // Validate dealerName to ensure it is not empty or null
        while (string.IsNullOrEmpty(dealerName))
        {
            Console.WriteLine("Dealer's name cannot be empty. Please enter again:");
            dealerName = Console.ReadLine()!.Trim();
        }

        Dealer = new Dealer(dealerName);
    }

    private void SetUpPlayers()
    {
        int numberOfPlayers = 0;
        string input;

        while (numberOfPlayers <= 0 || numberOfPlayers > _variables.MaxPlayers)
        {
            Console.WriteLine("Enter the number of players:");

            try
            {
                input = Console.ReadLine()!;
                numberOfPlayers = int.Parse(input);

                // Check if the parsed number is within the valid range
                if (numberOfPlayers <= 0 || numberOfPlayers > _variables.MaxPlayers)
                {
                    Console.WriteLine($"Number of players must be a positive integer less than or equal to {_variables.MaxPlayers}. Please enter again:");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input is too large. Please enter a smaller number.");
            }
        }

        for (int i = 0; i < numberOfPlayers; i++)
        {
            SetUpPlayer();
        }

    }

    private void SetUpPlayer()
    {
        Console.WriteLine("Enter the player's name:");

        string playerName = Console.ReadLine()!.Trim(); // Trim to remove leading and trailing whitespace

        // Validate playerName to ensure it is not empty or null
        while (string.IsNullOrEmpty(playerName))
        {
            Console.WriteLine("Player's name cannot be empty. Please enter again:");
            playerName = Console.ReadLine()!.Trim();
        }

        Player player = new Player(playerName);
        Players.Add(player);
    }

    private void DealerSetUp()
    {
        Console.WriteLine("What to do first: ");
        Console.WriteLine("1. Add deck");
        Console.WriteLine("2. Shuffle shoe");

    }
}