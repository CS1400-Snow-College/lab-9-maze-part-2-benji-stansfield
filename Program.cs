/*Benji Stansfield, 3-27-25, Lab 9 "Maze 2"*/
Console.Clear();

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

foreach (char[] character in mazeChar) //prints the maze map
{
    Console.WriteLine(character);
}

Console.SetCursorPosition(mazeChar[0].Length + 5, 0);
Console.Write($"Score: {score}");

Console.SetCursorPosition(playerLeft, playerTop); //sets the user to the beginning of the maze

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
            Console.WriteLine("------------");
            Console.WriteLine(" YOU WIN!!!");
            Console.WriteLine("------------");
            return;
        }

        playerLeft = proposedLeft;
        playerTop = proposedTop;
        Console.SetCursorPosition(playerLeft, playerTop); //updates user position

        Console.SetCursorPosition(mazeChar[0].Length + 5, 0);
        Console.Write($"Score: {score}   "); //updates the score on screen

        Console.SetCursorPosition(playerLeft, playerTop); //resets cursor back to where it was

        if (score >= 1000)
        {
            maze = File.ReadAllLines("maze2.txt");
            mazeChar = maze.Select(item => item.ToArray()).ToArray();
            Console.SetCursorPosition(0,0);

            foreach (char[] character in mazeChar) //prints the maze map
            {
                Console.WriteLine(character);
            }

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
    if (mazeChar[proposedTop][proposedLeft] == '*' ||mazeChar[proposedTop][proposedLeft] == '|')
        return false;

    return true;
}
