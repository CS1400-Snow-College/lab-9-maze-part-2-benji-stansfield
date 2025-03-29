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

foreach (char[] character in mazeChar) //prints the maze map
{
    Console.WriteLine(character);
}

Console.SetCursorPosition(0,0); //sets the user to the beginning of the maze

/*User controls*/
ConsoleKey key;
do
{
    key = Console.ReadKey(true).Key; //reads the user input one at a time

    int proposedTop = Console.CursorTop; //stores the proposed values to validate them
    int proposedLeft = Console.CursorLeft;

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
    TryMove(proposedLeft, proposedTop, maze);
} while (key != ConsoleKey.Escape); //quits the program if the escape key is pressed

static bool TryMove(int proposedLeft, int proposedTop, string[] mapRows) //need to create new variables because of the static in front
{
    if (proposedTop < 0 || proposedTop >= Math.Min(Console.BufferHeight, mapRows.Length))
        return false;
    if (proposedLeft < 0 || proposedLeft >= Math.Min(Console.BufferWidth, mapRows[proposedTop].Length))
        return false;
    if (mapRows[proposedTop][proposedLeft] == '*' || mapRows[proposedTop][proposedLeft] == '|')
        return false;

    Console.SetCursorPosition(proposedLeft, proposedTop); //sets the cursor position if all the checks pass
    return true;
}