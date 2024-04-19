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
        GameLoop();
    }

    private void SetUpGame()
    {
        SetUpDealer();
        SetUpPlayers();
        DealerSetUp();
    }

    private void GameLoop()
    {
        while (true)
        {
            Dealer.DisplayScore();

            // deal cards to players and dealer
            // check for blackjack
            // player turn
            // dealer turn
            // check for winner
            // start over or end game
        }
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
                    Console.WriteLine(
                        $"Number of players must be a positive integer less than or equal to {_variables.MaxPlayers}. Please enter again:");
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
        int setupStep = 0;
        while (setupStep != 2)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Add deck");
            Console.WriteLine("2. Shuffle shoe");

            string input = Console.ReadLine()!.Trim();

            switch (input)
            {
                case "1":
                case "Add deck":
                    if (setupStep == 0)
                    {
                        AddDecks();
                        setupStep++;
                        Dealer.AddScore(1);
                    }
                    else
                    {
                        Console.WriteLine("You have already added decks.");
                        Dealer.SubtractScore(2);
                    }
                    break;
                case "2":
                case "Shuffle shoe":
                    if (setupStep == 1)
                    {
                        Dealer.ShuffleShoe();
                        setupStep++;
                        Dealer.AddScore(1);
                    }
                    else
                    {
                        Console.WriteLine("Wait until you have added decks before shuffling.");
                        Dealer.SubtractScore(2);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid input. Please enter again.");
                    break;
            }
        }
    }

    private void AddDecks()
    {
        Console.WriteLine("How many decks to add?");
        string input = Console.ReadLine()!.Trim();

        int numberOfDecks;

        while (!int.TryParse(input, out numberOfDecks) || numberOfDecks <= _variables.MinDecks || numberOfDecks > _variables.MaxDecks)
        {
            if (!int.TryParse(input, out _))
            {
                Console.WriteLine("Invalid input. Please enter a positive integer.");
            }
            else if (numberOfDecks <= _variables.MinDecks)
            {
                Console.WriteLine($"The number of decks must be greater than {_variables.MinDecks}.");
            }
            else if (numberOfDecks > _variables.MaxDecks)
            {
                Console.WriteLine($"The number of decks must be less than or equal to {_variables.MaxDecks}.");
            }

            Console.WriteLine("Please enter a valid number of decks:");
            input = Console.ReadLine()!.Trim();
        }


        for (int i = 0; i < numberOfDecks; i++)
        {
            Dealer.Shoe.AddDeck(new Deck());
        }
    }
}