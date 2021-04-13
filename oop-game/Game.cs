using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    class Game
    {
        private World TheWorld;
        private Player ThePlayer;

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
                        ThePlayer.Y -= 1;
                    }
                    ThePlayer.DrawModel();
                    break;
                case ConsoleKey.DownArrow:
                    if (TheWorld.Walkable(ThePlayer.X, ThePlayer.Y + 1))
                    {
                        ThePlayer.Y += 1;
                    }
                   
                    break;
                case ConsoleKey.LeftArrow:
                    if (TheWorld.Walkable(ThePlayer.X - 1, ThePlayer.Y))
                    {
                        ThePlayer.X -= 1;
                    }
                    
                    break;
                case ConsoleKey.RightArrow:
                    if (TheWorld.Walkable(ThePlayer.X + 1, ThePlayer.Y))
                    {
                        ThePlayer.X += 1;
                    }                    
                    break;
                default:
                    
                    break;
            }
        }
        private void GameLoop()
        {
            
            while (true)
            {
                TheWorld.Draw();
                ThePlayer.DrawModel();
                PlayerInput();
            }
        }
    }
}
