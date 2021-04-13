using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    class Game
    {
        private World TheWorld;
        private Player ThePlayer;
        byte[] buffer = Levelreader.ReadFileToByteArray("TextFile1.txt");

        public void Start()
        {
            string[,] grid = Levelreader.ReadFileToArray("TextFile1.txt");
            TheWorld = new World(grid);
            ThePlayer = new Player(1, 1);
            Console.CursorVisible = false;
            GameLoop();
        }

        private void PlayerInput()
        {
            ConsoleKeyInfo keypress = Console.ReadKey(true);
            ConsoleKey key = keypress.Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (TheWorld.Walkable(ThePlayer.X, ThePlayer.Y - 1))
                    {
                        ThePlayer.DrawModel(' ');
                        ThePlayer.Y -= 1;
                    }
                    ThePlayer.DrawModel();
                    break;
                case ConsoleKey.DownArrow:
                    if (TheWorld.Walkable(ThePlayer.X, ThePlayer.Y + 1))
                    {
                        ThePlayer.DrawModel(' ');
                        ThePlayer.Y += 1;
                    }
                   
                    break;
                case ConsoleKey.LeftArrow:
                    if (TheWorld.Walkable(ThePlayer.X - 1, ThePlayer.Y))
                    {
                        ThePlayer.DrawModel(' ');
                        ThePlayer.X -= 1;
                    }                   
                    break;
                case ConsoleKey.RightArrow:
                    if (TheWorld.Walkable(ThePlayer.X + 1, ThePlayer.Y))
                    {
                        ThePlayer.DrawModel(' ');
                        ThePlayer.X += 1;
                    }                    
                    break;
                default:                   
                    break;
            }
        }
        private void GameLoop()
        {
            TheWorld.FastDraw(buffer);
            while (true)
            {
                
                ThePlayer.DrawModel();
                PlayerInput();
            }
        }
    }
}
