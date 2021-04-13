using System;
using System.Threading;

namespace oop_game
{
    class Program
    {
        static void Main(string[] args)
        {
            Game newGame = new Game();
            newGame.Start();
            
           /* World myWorld = new World(grid);
            Player newPlayer = new Player(1, 1);
            var playerX = 1;
            var playerY = 1; // Contains current cursor position.
            
            //myWorld.Draw();
            DrawPlayer(myWorld, 'O', playerX, playerY);
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
                                    playerY++;
                                }
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (string.IsNullOrWhiteSpace(myWorld.Grid[playerY - 1, playerX]))
                            {
                                if (playerY > 0)
                                {
                                    playerY--;
                                }
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (string.IsNullOrWhiteSpace(myWorld.Grid[playerY, playerX - 1]))
                            {
                                if (playerX > 0)
                                {
                                    playerX--;
                                }
                            }

                            break;
                        case ConsoleKey.RightArrow:
                            if (string.IsNullOrWhiteSpace(myWorld.Grid[playerY, playerX + 1]))
                            {
                                playerX++;
                            }
                            break;
                    }
                   
                    DrawPlayer(myWorld, 'O', playerX, playerY);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }
        public static void DrawPlayer(World myworld, char toWrite, int x = 0, int y = 0)
        {
            try
            {
                if (x >= 0 && y >= 0) // 0-based
                {
                    //Console.Clear();
                    myworld.Draw();
                    Console.SetCursorPosition(x, y);
                    Console.Write(toWrite);
                }
            }
            catch (Exception)
            {
            }*/
        }
       
    }

}
