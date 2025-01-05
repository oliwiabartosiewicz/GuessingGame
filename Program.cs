namespace AssessmentTask;

class Program
{
    
    const int boardSize = 3;
    static char[,] board = new char[boardSize, boardSize];
    
    static void Main()
    {
        Console.WriteLine("Welcome to the memory game!");
        Console.WriteLine("There will be a board shown to you, " +
                          "you then have to reveal characters and rememeber where each is placed" +
                          " when you get a match they will stay revealed until you find all matching");

    }

    static void ResetBoard()
    {

        char[] symbols = new char[]
            { 'A', 'A', 'B', 'B', 'C', 'C', 'D', 'D', 'E', 'E', 'F', 'F' };

    }

}