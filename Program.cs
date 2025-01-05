namespace AssessmentTask;

class Program
{

    const int boardSize = 3;
    static Random generator = new Random();
    static char[,] board = new char[boardSize, boardSize];
    static bool[,] revealed = new bool[boardSize, boardSize];

    static void Main()
    {
        Console.WriteLine("Welcome to the memory game!");
        Console.WriteLine("The game begins with all cards facing down. Players take turns flipping over two cards." +
                          "If the two cards match, the player keeps them, otherwise, the cards are flipped back down. " +
                          "Once all cards are matched, you win!");

        DisplayBoard();
    }

    static void DisplayBoard()
    {
        Console.WriteLine("  0 1 2");
        for (int row = 0; row < boardSize; row++)
        {
            Console.Write(row + " ");
            for (int collumn = 0; collumn < boardSize; collumn++)
            {
                if (revealed[row, collumn])
                {
                    Console.Write(board[row, collumn] + " ");
                }
                else
                {
                    Console.Write("* ");
                }
            }
            Console.WriteLine();
        }
    }
}
