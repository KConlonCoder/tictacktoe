using System;
using System.ComponentModel.Design;
using System.Linq;

// Delcare variables
string[] Players = {"test1", "test2"};
string[] GameMarkers = { "X", "O" };
string outcome = "";


// Call new game (main)
newGame(Players, outcome);

////////////////////////////////////////////////////////////
// Functions
////////////////////////////////////////////////////////////

void newGame(string[] Players, string outcome) {

    Console.WriteLine("Are you ready to play Tic Tack Toe (Y/N)?");

    string inputReadiness = Console.ReadLine().Trim().ToLower();

    while (!new[] { "yes", "y", "no", "n" }.Contains(inputReadiness)) {

        Console.WriteLine("Invalid entry. Please try again.\n");

        Console.WriteLine("Are you ready to play Tic Tack Toe (Y/N)?");

        inputReadiness = Console.ReadLine().Trim().ToLower();
    }

    if (new[] { "yes", "y" }.Contains(inputReadiness))
    {
        // Reset the game board
        string[] Moves = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        // Get player 1's name
        Console.WriteLine("\nEnter Player 1's name.");
        Players[0] = Console.ReadLine().Trim();

        // Get player 2's name
        Console.WriteLine("\nEnter Player 2's name.");
        Players[1] = Console.ReadLine().Trim();

        // Print game board
        Gameboard(Moves);

        // Start game
        TakeTurn(Players, Moves);
    }
    else if (new[] { "no", "n" }.Contains(inputReadiness))
    {
        Console.WriteLine("Goodbye.\n");
    }

}


string[] TakeTurn(string[] Players, string[] Moves)
{

    // Continue playing until this game's over
    while (outcome == "")
    {
        // Players take turns
        foreach (string Player in Players)
        {
            // Don't continue taking turns if game is finished
            if (outcome == "")
            {
                string ValidEntry = "No";

                // Loop until player makes a valid entry or game is over
                while (ValidEntry == "No" && outcome == "")
                {
                    // Instructions
                    Console.WriteLine($"{Player}, pick a number to make your move.\n");

                    // Collect move
                    string move = Console.ReadLine().Trim();

                    // Check valid entry: move is a number between 0-8, that number on the board hasn't been taken
                    if (
                            // Create int version of move as m
                            Int32.TryParse(move, out int m)
                            // Check if it's between 0-8 - i.e. a valid selection
                            && Enumerable.Range(0, 9).Contains(m - 1)
                            // Check that location on the board hasn't already been played
                            && Int32.TryParse(Moves[m - 1], out int n)
                        )
                    {
                        // While loop will end bc of this
                        ValidEntry = "Yes";

                        // Update game board with the player's game marker
                        Moves[m - 1] = GameMarkers[Array.IndexOf(Players, Player)];

                        // Output game board
                        Gameboard(Moves);

                        // Check if someone won or there's a tie
                        CheckForWinner(Moves);
                    }
                    else
                    {
                        Console.WriteLine("Invalid entry. Please try again.\n");
                    }

                }
            }
        }
    }
    // If game is over, reset the outcome
    if (outcome != "")
    {
        // Reset outcome and game board
        outcome = "";

        // Ask if they want to play again
        newGame(Players, outcome);
    }

    return Moves;
}

void Gameboard(string[] Moves)
{
    Console.WriteLine(
        "\n" +
        $"{Moves[0]} | {Moves[1]} | {Moves[2]}\n" +
        "--|---|--\n" +
        $"{Moves[3]} | {Moves[4]} | {Moves[5]}\n" +
        "--|---|--\n" +
        $"{Moves[6]} | {Moves[7]} | {Moves[8]}\n");
}
string CheckForWinner(string[] Moves)
{

    if ( // Check for winner
         // Check horizontally
        new[] { "XXX", "OOO" }.Contains(string.Concat(Moves[0], Moves[1], Moves[2]))
        || new[] { "XXX", "OOO" }.Contains(string.Concat(Moves[3], Moves[4], Moves[5]))
        || new[] { "XXX", "OOO" }.Contains(string.Concat(Moves[6], Moves[7], Moves[8]))
        // Check vertically
        || new[] { "XXX", "OOO" }.Contains(string.Concat(Moves[0], Moves[3], Moves[6]))
        || new[] { "XXX", "OOO" }.Contains(string.Concat(Moves[1], Moves[4], Moves[7]))
        || new[] { "XXX", "OOO" }.Contains(string.Concat(Moves[2], Moves[5], Moves[8]))
        // Check diagonally
        || new[] { "XXX", "OOO" }.Contains(string.Concat(Moves[0], Moves[4], Moves[8]))
        || new[] { "XXX", "OOO" }.Contains(string.Concat(Moves[2], Moves[4], Moves[6]))
        )
    {
        Console.WriteLine("Winner!\n");
        outcome = "winner";

    }
    else if ( // If board is full but there wasn't a winner, then it's a tie
        string.Join("", Moves).Replace("X", "").Replace("O", "") == ""
        )
    {
        Console.WriteLine("It's a tie.\n");
        outcome = "tie";

    }
    else // Game is still on
    {
        Console.WriteLine("Next turn.\n");
        outcome = "";
    }

    return outcome;
}
