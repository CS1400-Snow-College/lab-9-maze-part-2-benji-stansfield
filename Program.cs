/*Benji Stansfield, 3-27-25, Lab 9 "Maze 2"*/
using System.Diagnostics;
Console.Clear();

Stopwatch stopwatch = new Stopwatch();

/*Intro screen*/
Console.WriteLine(@"-------------
 MAZE GAME 2
-------------
");

Console.WriteLine(@"Avoid the bad guys (%) and collect as much money (^ or $) as possible. Unlock the door to reach the '#' as fast as possible.
Press any key to begin.");
Console.ReadKey(true);
Console.Clear();

/*Add the map*/
string[] maze = File.ReadAllLines("maze.txt");
char[][] mazeChar = maze.Select(item => item.ToArray()).ToArray(); //thank you for this code!!!

int score = 0;
int playerLeft = 0;
int playerTop = 0;

void PrintMaze()
{
    Console.Clear();
    foreach (char[] character in mazeChar)
    {
        Console.WriteLine(character);
    }

    Console.SetCursorPosition(mazeChar[0].Length + 5, 0);
    Console.Write($"Score: {score}");
}

PrintMaze();

Console.SetCursorPosition(playerLeft, playerTop); //sets the user to the beginning of the maze

stopwatch.Start();

/*User controls*/
ConsoleKey key;
do
{
    key = Console.ReadKey(true).Key; //reads the user input one at a time

    int proposedTop = playerTop; //stores the proposed values to validate them
    int proposedLeft = playerLeft;

    switch (key)
    {
        case ConsoleKey.UpArrow:
            proposedTop--;
            break;
        case ConsoleKey.DownArrow:
            proposedTop++;
            break;
        case ConsoleKey.LeftArrow:
            proposedLeft--;
            break;
        case ConsoleKey.RightArrow:
            proposedLeft++;
            break;
    }
    
    if (TryMove(proposedLeft, proposedTop, mazeChar))
    {   
        Console.SetCursorPosition(playerLeft, playerTop);
        Console.Write(' '); //eraseses user's previous position

        if (mazeChar[proposedTop][proposedLeft] == '^')
        {
            score += 100;
            mazeChar[proposedTop][proposedLeft] = ' ';
        }
        else if (mazeChar[proposedTop][proposedLeft] == '$')
        {
            score += 200;
            mazeChar[proposedTop][proposedLeft] = ' ';
        }
        else if (mazeChar[proposedTop][proposedLeft] == '#')
        {
            Console.Clear();
            stopwatch.Stop();
            double time = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.WriteLine("------------");
            Console.WriteLine(" YOU WIN!!!");
            Console.WriteLine("------------");
            Console.WriteLine();
            Console.WriteLine($"Money collected: {score}");
            Console.WriteLine($"Time: {time} seconds");
            Console.WriteLine($"Total score: {score - (time*10)}");
            return;
        }
        else if (mazeChar[proposedTop][proposedLeft] == '%')
        {
            Console.Clear();
            stopwatch.Stop();
            double time = stopwatch.ElapsedMilliseconds / 1000.0;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("-----------");
            Console.WriteLine(" YOU LOSE.");
            Console.WriteLine("-----------");
            Console.WriteLine("You were killed by an enemy.");
            Console.WriteLine();
            Console.WriteLine($"Money collected: {score}");
            Console.WriteLine($"Time: {time} seconds");
            Console.WriteLine($"Total score: {score - (time*10)}");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        playerLeft = proposedLeft;
        playerTop = proposedTop;

        PrintMaze();

        Console.SetCursorPosition(playerLeft, playerTop); //updates user position

        Console.SetCursorPosition(mazeChar[0].Length + 5, 0);
        Console.Write($"Score: {score}   "); //updates the score on screen

        Console.SetCursorPosition(playerLeft, playerTop); //resets cursor back to where it was

        if (score >= 1000)
        {
            maze = File.ReadAllLines("maze2.txt");
            mazeChar = maze.Select(item => item.ToArray()).ToArray();

            PrintMaze();

            Console.SetCursorPosition(playerLeft, playerTop);
        }
    }
} while (key != ConsoleKey.Escape); //quits the program if the escape key is pressed

static bool TryMove(int proposedLeft, int proposedTop, char[][] mazeChar) //need to create new variables because of the static in front
{
    if (proposedTop < 0 || proposedTop >= Math.Min(Console.BufferHeight, mazeChar.Length))
        return false;
    if (proposedLeft < 0 || proposedLeft >= Math.Min(Console.BufferWidth, mazeChar[proposedTop].Length))
        return false;
    if (mazeChar[proposedTop][proposedLeft] == '*' || mazeChar[proposedTop][proposedLeft] == '|')
        return false;
    if (mazeChar[proposedTop][proposedLeft] == '%')
        return true;

    return true;
}

/*I know that I am missing a few things on this project but I couldn't figure them out. Namely collecting the '$' and moving the bad guys.*/
/*Any help or feedback on this project would be really helpful as I really do want to know how to do this*/
