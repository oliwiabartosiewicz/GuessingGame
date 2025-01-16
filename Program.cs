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
        Console.WriteLine("the cards are flipped back down. You will have a limited amount of guesses!");
        Console.WriteLine("Once all cards are matched, you win!");
        Console.WriteLine();
        Thread.Sleep(1000);
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
        { // the below 4 lines was done with help of ChatGPT    
            int random = generator.Next(0, i + 1);
            char character = boardWithSymbols[i];
            boardWithSymbols[i] = boardWithSymbols[random];
            boardWithSymbols[random] = character;
        }
        int index = 0;
        for (int row = 0; row < boardSize; row++)
        {
            for (int column = 0; column < boardSize; column++)
            {//assigns symbol from shuffled array to the board position
                //the next line is adapted with ChatGPT 
                board[row, column] = boardWithSymbols[index];
                revealed[row, column] = false;
                index++;
            }
        }
    }
    
    static void GetUserInput()
    {
        int score= 0;
        while (!CheckAllCardsRevealed())
        {//score is greater than or equal to
            StreamReader configFile = new StreamReader("config.txt");
            int maxAttempts = int.Parse(configFile.ReadLine());
            if (score >= maxAttempts)
            {
            Console.WriteLine("You've reached the maximum number of guesses allowed. Game over!");
            return;
            }
            configFile.Close();
            // first choice of card
            int rowFirstCard; 
            int columnFirstCard;
            while (true)
            {
                rowFirstCard = readInt("Enter Row for first card (0 to " + (boardSize - 1) + "): ", 0,
                    boardSize - 1);
                columnFirstCard = readInt("Enter Column for first card (0 to " + (boardSize - 1) + "): ", 0,
                    boardSize - 1);
                if (!revealed[rowFirstCard, columnFirstCard])
                {
                    break;
                }

                Console.WriteLine("This card is already revealed. Please choose another card.");
            }
            revealed[rowFirstCard, columnFirstCard] = true;
            Console.Clear();
            DisplayBoard();
            
            // second choice of card
            int rowSecondCard; 
            int columnSecondCard;
            while (true)
            {
                rowSecondCard = readInt("Enter Row for second card (0 to " + (boardSize - 1) + "): ", 0,
                    boardSize - 1);
                columnSecondCard = readInt("Enter Column for second card (0 to " + (boardSize - 1) + "): ", 0,
                    boardSize - 1);
                if (!revealed[rowFirstCard, columnFirstCard]||!revealed[rowSecondCard, columnSecondCard])
                {
                    break;
                }

                Console.WriteLine("This card is already revealed. Please choose another card.");
            }
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
            score++;
            
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
            DisplayBoard();
        }
        Console.WriteLine("You have matched all cards!");
        Console.WriteLine("You finished the game in "  + score +" guesses.");
        askingUserNameAndScore(score);
        
    }

    static bool CheckAllCardsRevealed()
        {//looping through every row 
            for (int row = 0; row < boardSize; row++)
            {//looping through every column in row
                for (int column = 0; column < boardSize; column++)
                {
                    if (!revealed[row, column])
                    {
                        return false;//If card is still hidden, then exits and returns false.
                    }
                }
            }
            return true;
        }
    
    static void askingUserNameAndScore(int score)
    {
        string userName = readString("What is your name? This is so that we can record your result: ");
        StreamWriter scoreFileToUpdate = new StreamWriter(previousPlayerScores,true);
        StreamReader configFile = new StreamReader("config.txt");
        int maxAttempts = int.Parse(configFile.ReadLine());
            string guessesused = score + "/" + maxAttempts;
            scoreFileToUpdate.WriteLine(userName + ": " + guessesused);
        configFile.Close();
        scoreFileToUpdate.Close();
    }
// For the below readString Miles, R. (2019) C# Programming Yellow Book. 8th edn. Section 3.1.10 Method Libraries, pp. 56–58.
// // Available at: www.robmiles.com.
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
    
    static int readInteger(string prompt)
    {
        do
        {
            string enteredString = readString(prompt);
            try
            {
                return int.Parse(enteredString);
            }
            catch
            {
                Console.WriteLine("Invalid integer input! Please enter a valid integer.");
            }
        } while (true);
    }
// For the below readInt Miles, R. (2019) C# Programming Yellow Book. 8th edn. Section 3.1.10 Method Libraries, pp. 56–58.
// Available at: www.robmiles.com.
    static int readInt(string prompt, int low, int high)
    {
        int result;
        do
        {
            result = readInteger(prompt);
            Console.WriteLine();
            if ((result < low) || (result > high))
            {
                Console.WriteLine("Number out of range.");
            }
        } while ((result < low) || (result > high));
        return result;
    }
    //Sculthorpe,N.(2024).Fundamentals of Software Programming (SOFT40121) 
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
    //Sculthorpe,N.(2024). Fundamentals of Software Programming (SOFT40121) Programming Exercises: File Output
    static void showOldScores()
        {
            if (File.Exists(previousPlayerScores))
            {
                StreamReader previousPlayersFile = new StreamReader(previousPlayerScores);
                Console.WriteLine("Previous player scores:");
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