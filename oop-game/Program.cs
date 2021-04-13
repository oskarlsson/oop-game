﻿using System;
using System.Threading;

namespace oop_game
{
    class Program
    {
        static void Main(string[] args)
        {


            Console.SetWindowSize(240, 63);
            string[,] grid = Levelreader.ReadFileToArray("TextFile1.txt");
            byte[] buffer = Levelreader.ReadFileToByteArray("TextFile1.txt");
            World myWorld = new World(grid);
            var playerX = 1;
            var playerY = 1; // Contains current cursor position.
            Console.CursorVisible = false;
            myWorld.FastDraw(buffer);
            DrawPlayer(myWorld, 'c',buffer, playerX, playerY);
            while (true)
            {
                

                if (Console.KeyAvailable)
                {
                    var command = Console.ReadKey().Key;

                    switch (command)
                    {
                        case ConsoleKey.DownArrow:
                            if (string.IsNullOrWhiteSpace(myWorld.Grid[playerY + 1, playerX]))
                            {
                                if (playerY > 0)
                                {
                                    DrawPlayer(myWorld, ' ', buffer, playerX, playerY);
                                    playerY++;
                                }
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (string.IsNullOrWhiteSpace(myWorld.Grid[playerY - 1, playerX]))
                            {
                                if (playerY > 0)
                                {
                                    DrawPlayer(myWorld, ' ', buffer, playerX, playerY);
                                    playerY--;
                                }
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (string.IsNullOrWhiteSpace(myWorld.Grid[playerY, playerX - 1]))
                            {
                                if (playerX > 0)
                                {
                                    DrawPlayer(myWorld, ' ', buffer, playerX, playerY);
                                    playerX--;
                                }
                            }

                            break;
                        case ConsoleKey.RightArrow:
                            if (string.IsNullOrWhiteSpace(myWorld.Grid[playerY, playerX + 1]))
                            {
                                DrawPlayer(myWorld, ' ', buffer, playerX, playerY);
                                playerX++;
                            }
                            break;
                    }
                   
                    DrawPlayer(myWorld, 'c',buffer, playerX, playerY);
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }
        public static void DrawPlayer(World myworld, char toWrite, byte[] buffer, int x = 0, int y = 0)
        {
            try
            {
                if (x >= 0 && y >= 0) // 0-based
                {

                    //Console.Clear();
                    //myworld.Draw();
                    Console.SetCursorPosition(x, y);
                    Console.Write(toWrite);
                }
            }
            catch (Exception)
            {
            }
        }
    }

}
