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

    switch (key)
    {
        case ConsoleKey.UpArrow:
            Console.CursorTop--;
            break;
        case ConsoleKey.DownArrow:
            Console.CursorTop++;
            break;
        case ConsoleKey.LeftArrow:
            Console.CursorLeft--;
            break;
        case ConsoleKey.RightArrow:
            Console.CursorLeft++;
            break;
    }
} while (key != ConsoleKey.Escape); //quits the program if the escape key is pressed
