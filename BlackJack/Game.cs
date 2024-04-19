using System.Diagnostics;

namespace BlackJack;

public class Game
{
    readonly Variables _variables = new Variables();

    private Dealer Dealer { get; set; } = null!;

    private List<Player> Players { get; set; } = new List<Player>();

    private Random rng = new Random();


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
            int gameStep = 1;
            while (gameStep < 5)
            {
                Dealer.DisplayScore();
                Console.WriteLine(gameStep);

                Console.WriteLine("Would you like to play do?");
                Console.WriteLine("1. Deal initial cards");
                Console.WriteLine("2. Do Insurance bet");
                Console.WriteLine("3. Player turn");
                Console.WriteLine("4. Dealer turn");
                String input = Console.ReadLine()!.Trim().ToLower();

                switch (input)
                {
                    case "1":
                    case "deal initial cards":
                        if (gameStep == 1)
                        {
                            DealInitialCards();
                            gameStep++;
                            Dealer.AddScore(1);
                        }
                        else
                        {
                            Console.WriteLine("You have already dealt the initial cards.");
                            Dealer.SubtractScore(2);
                        }
                        break;
                    case "2":
                    case "do insurance bet":
                        if (gameStep == 2)
                        {
                            InsuranceCase();
                            gameStep++;
                            Dealer.AddScore(1);
                        }
                        else
                        {
                            Console.WriteLine("You have already done the insurance bet.");
                            Dealer.SubtractScore(2);
                        }
                        break;
                    case "3":
                    case "player turn":
                        if (gameStep == 3)
                        {
                            foreach (var player in Players)
                            {
                                PlayerTurn(player);
                            }
                            gameStep++;
                            Dealer.AddScore(1);
                        }
                        else
                        {
                            Console.WriteLine("You have already done the player turn.");
                            Dealer.SubtractScore(2);
                        }
                        break;
                    case "4":
                    case "dealer turn":
                        if (gameStep == 4)
                        {
                            DealerTurn();
                            gameStep++;
                            Dealer.AddScore(1);
                        }
                        else
                        {
                            Console.WriteLine("You have already done the dealer turn.");
                            Dealer.SubtractScore(2);
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter again.");
                        break;
                }
            }

            bool playAgain = AskToPlayAgain();

            if (!playAgain)
            {
                Console.WriteLine("Thanks for playing!");
                Dealer.DisplayScore();
                break;
            }
        }
    }

    private bool AskToPlayAgain()
    {
        Console.WriteLine("Would you like to play again? (yes/no)");
        string input = Console.ReadLine()!.Trim().ToLower();

        while (String.IsNullOrEmpty(input) || (input != "yes" && input != "no"))
        {
            Console.WriteLine("Invalid input. Please enter again.");
            input = Console.ReadLine()!.Trim().ToLower();
        }

        return input == "yes";
    }

    private void DealerTurn()
    {
        while (Dealer.GetPlayer().Hands[0].GetHandValue() < 17)
        {
            Dealer.DealCard(Dealer.GetPlayer(), 0);
        }

        Dealer.GetPlayer().DisplayHand();

        if (Dealer.GetPlayer().Hands[0].IsBusted())
        {
            Console.WriteLine("Dealer busted!");

            foreach (var player in Players)
            {
                foreach (var hand in player.Hands)
                {
                    if (!hand.IsBusted())
                    {
                        player.AddMoney(hand.Bet * 2);
                    }
                }
            }

            return;
        }

        foreach (var player in Players)
        {
            foreach (var hand in player.Hands)
            {
                if (hand.GetHandValue() > Dealer.GetPlayer().Hands[0].GetHandValue())
                {
                    player.AddMoney(hand.Bet * 2);
                }
                else if (hand.GetHandValue() == Dealer.GetPlayer().Hands[0].GetHandValue())
                {
                    player.AddMoney(hand.Bet);
                }
            }
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

        while (!int.TryParse(input, out numberOfDecks) || numberOfDecks <= _variables.MinDecks ||
               numberOfDecks > _variables.MaxDecks)
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
            Console.WriteLine("Adding deck...");
            Dealer.Shoe.AddDeck(new Deck());
        }
    }

    private void DealInitialCards()
    {
        for (int i = 0; i < _variables.StartingHandSize; i++)
        {
            foreach (var player in Players)
            {
                Dealer.DealCard(player, 0);
            }

            Dealer.DealCard(Dealer.GetPlayer(), 0);
        }

        foreach (var player in Players)
        {
            player.DisplayHand();
        }
    }

    private void InsuranceCase()
    {
        if (Dealer.GetPlayer().Hands[0].Cards[0].GetValue() != 1)
        {
            return;
        }

        var playersWithOutInsurance = new List<Player>();

        foreach (var player in Players)
        {
            if (rng.Next(0, 3) == 1)
            {
                player.PlaceInsuranceBet();
            }
            else
            {
                playersWithOutInsurance.Add(player);
            }
        }

        if (Dealer.GetPlayer().Hands[0].IsBlackJack())
        {
            foreach (var player in Players)
            {
                if (player.Insurance)
                {
                    player.WinInsuranceBet();
                }
            }
        }
        else
        {
            foreach (var player in playersWithOutInsurance)
            {
                player.LoseInsuranceBet();
            }
        }

        foreach (var player in Players)
        {
            player.Insurance = false;
        }
    }

    private void PlayerTurn(Player player, int handIndex = 0)
    {
        while (true)
        {
            int hitChange = player.Hands[handIndex].GetHandValue() - 17;

            if (player.Hands[0].IsBlackJack())
            {
                Console.WriteLine("Blackjack!");
                player.AddMoney(player.Hands[handIndex].Bet * 1.5f);
                return;
            }

            if (player.Hands[handIndex].Cards[0].GetValue() == player.Hands[handIndex].Cards[1].GetValue())
            {
                if (rng.Next(0, 3) == 1)
                {
                    player.Split();
                    PlayerSplitCase(player, handIndex);
                    PlayerTurn(player, handIndex);
                    handIndex++;
                    continue;
                }
            }

            if (hitChange > 0)
            {
                player.Stand();
            }
            else if (hitChange < 0)
            {
                player.Hit(Dealer.Shoe.DrawCard(), handIndex);
            }
            else
            {
                if (rng.Next(0, 3) == 1)
                {
                    player.DoubleDown(Dealer.Shoe.DrawCard(), handIndex);
                }
                else
                {
                    player.Hit(Dealer.Shoe.DrawCard(), handIndex);
                }
            }

            if (player.Hands[handIndex].IsBusted())
            {
                Console.WriteLine("Busted!");

                if (handIndex != 0)
                {
                    player.Hands.RemoveAt(handIndex);
                }
                else
                {
                    player.Hands[handIndex].Bet = 0;
                }
            }

            break;
        }
    }

    private void PlayerSplitCase(Player player, int handIndex)
    {
        player.AddCardToHand(Dealer.Shoe.DrawCard(), handIndex);
        player.AddCardToHand(Dealer.Shoe.DrawCard(), handIndex + 1);
    }
}