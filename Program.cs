namespace AssessmentTask;
using System;
using System.IO;

class Program
{
    const int boardSize = 4;
    static Random generator = new Random();
    static char[,] board = new char[boardSize, boardSize];
    static bool[,] revealed = new bool[boardSize, boardSize];
    const string memoryGameSymbolFile = "memoryGameSymbol.txt";
    const string previousPlayerScores = "previousPlayerScores.txt";

    static void Main()
    {
        WelcomeToMemoryGame();
        PreviousPlayerScores();
        ResetBoard();
        Console.Clear();
        DisplayBoard();
        GetUserInput();
    }


    static void WelcomeToMemoryGame()
    {
        Console.WriteLine("Welcome to the memory game!");
        Console.WriteLine("The game begins with all cards facing down. ");
        Console.WriteLine("Players take turns flipping over two cards.");
        Console.WriteLine("If the two cards match, the player keeps them, otherwise,");
        Console.WriteLine("the cards are flipped back down");
        Console.WriteLine("Once all cards are matched, you win!");
        Console.WriteLine();
        Thread.Sleep(2000);

    }

    

    static void DisplayBoard()
    {
        Console.WriteLine("  0 1 2 3");
        int row = 0;
        while (row < boardSize)
        {
            Console.Write(row + " ");
            int column = 0;
            while (column < boardSize)
            {
                if (revealed[row, column])
                {
                    Console.Write(board[row, column] + " ");
                }
                else
                {
                    Console.Write("* ");
                }
                column++;
            }
            Console.WriteLine();
            row++;
        }
    }

    static void ResetBoard()
    {
        StreamReader memorygameFile = new StreamReader(memoryGameSymbolFile);
        // counting the lines in the file until no more.
        int line_counter = 0;
        while (memorygameFile.ReadLine() != null)
        {
            line_counter++;
        }
        char[] boardWithSymbols = new char [line_counter];
        memorygameFile = new StreamReader(memoryGameSymbolFile);
        for (int i = 0; i < line_counter; i++)
        {
            boardWithSymbols[i] = memorygameFile.ReadLine()[0];
        }

        memorygameFile.Close();
        
// shuffling symbols from file uploaded
        for (int i = boardWithSymbols.Length - 1; i > 0; i--)
        {
            int random = generator.Next(0, i + 1);
            char character = boardWithSymbols[i];
            boardWithSymbols[i] = boardWithSymbols[random];
            boardWithSymbols[random] = character;
        }

        int index = 0;
        for (int row = 0; row < boardSize; row++)
        {
            for (int column = 0; column < boardSize; column++)
            {
                board[row, column] = boardWithSymbols[index];
                revealed[row, column] = false;
                index++;
            }
        }
    }



    static void GetUserInput()
    {
        CheckAllCardsRevealed();
        
        // first choice of card
        Console.Write("Enter Row for first card (0 to " + (boardSize - 1) + "): ");
        int rowFirstCard = int.Parse(Console.ReadLine());
        Console.Write("Enter Column for first card (0 to " + (boardSize - 1) + "): ");
        int columnFirstCard = int.Parse(Console.ReadLine());
        revealed[rowFirstCard, columnFirstCard] = true;
        Console.Clear();
        DisplayBoard();
        
    // second choice of card
        Console.Write("Enter Row for second card (0 to " + (boardSize - 1) + "): ");
        int rowSecondCard = int.Parse(Console.ReadLine());
        Console.Write("Enter Column for second card (0 to " + (boardSize - 1) + "): ");
        int columnSecondCard = int.Parse(Console.ReadLine());
        revealed[rowSecondCard, columnSecondCard] = true;
        Console.Clear();
        DisplayBoard();

        //match or no match
        if (board[rowFirstCard, columnFirstCard] == board[rowSecondCard, columnSecondCard])
        {
                Console.WriteLine("It's a match!");
        }
        else
        {
                Console.WriteLine("Not a match. Try again.");
                revealed[rowFirstCard, columnFirstCard] = false;
                revealed[rowSecondCard, columnSecondCard] = false;
        }
        
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
        Console.Clear();
    }
    

    
        static bool CheckAllCardsRevealed()
        {//looping through every row 
            for (int row = 0; row < boardSize; row++)
            {//looping through every column in row
                for (int column = 0; column < boardSize; column++)
                {
                    if (!revealed[row, column])
                    {
                        return false;
                    }
                }
            }
            return true;

            Console.WriteLine("Congratulations all cards are matched");
        }








        static string readString ( string prompt )
        {
            string result ;
            do
            {
                Console.Write ( prompt );
                result = Console.ReadLine ();
            } while ( result == "" );
            return result;
        }
        
        static void PreviousPlayerScores()
        {
            string previousPlayerScores = readString("Would you like to see previous player scores (y/n)?");
            switch (previousPlayerScores.ToLower())
            {
                case "yes":
                case "y":
                    showOldScores();
                    break;
                case "no":
                case "n":
                    Console.WriteLine("Starting the game... Press any key to continue.");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Invalid command. Starting the game...");
                    break;
            }

        }

        static void showOldScores()
        {
            if (File.Exists(previousPlayerScores))
            {
                StreamReader previousPlayersFile = new StreamReader(previousPlayerScores);
                Console.WriteLine("Previous player score:");
                foreach (string line in File.ReadAllLines(previousPlayerScores))
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("No previous player scores found.");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

}