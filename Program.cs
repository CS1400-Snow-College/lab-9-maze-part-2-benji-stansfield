﻿/*Benji Stansfield, 3-27-25, Lab 9 "Maze 2"*/
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

foreach (char[] character in mazeChar)
{
    Console.WriteLine(character);
}
