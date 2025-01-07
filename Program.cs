namespace AssessmentTask;
using System;
using System.IO;
class Program
{
    const int boardSize = 4;
   // static int totalNumberOfSymbols = 16;
    static Random generator = new Random();
    static char[,] board = new char[boardSize, boardSize];
    static bool[,] revealed = new bool[boardSize, boardSize];
    const string memoryGameSymbolFile = "memoryGameSymbol.txt";
    const string previousPlayerScores= "previousPlayerScores.txt";

    static void Main()
    {
        Console.WriteLine("Welcome to the memory game!");
        Console.WriteLine("The game begins with all cards facing down. ");
        Console.WriteLine("Players take turns flipping over two cards.");
        Console.WriteLine("If the two cards match, the player keeps them, otherwise,");
        Console.WriteLine("the cards are flipped back down");
        Console.WriteLine("Once all cards are matched, you win!");
        Console.WriteLine();
        
       
        ResetBoard();
        DisplayBoard();
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
        
       // totalNumberOfSymbols = int.Parse(memorygameFile.ReadLine());

        char[] boardWithSymbols = new char [line_counter];
        
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

    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    static void showOldScores()
    {
        if (File.Exists(previousPlayerScores))
        {
            StreamReader previousPlayersFile = new StreamReader(previousPlayerScores);
            Console.WriteLine("Previous player score:");
            while (previousPlayersFile.EndOfStream == false)
            {
                string line = previousPlayersFile.ReadLine();
                Console.WriteLine(line);
            }

            previousPlayersFile.Close();
        }
    }
}
