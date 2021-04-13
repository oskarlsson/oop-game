using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    class Game
    {
        private static Maze TheWorld;
        private Player ThePlayer;
        //byte[] buffer = Levelreader.ReadFileToByteArray("TextFile1.txt");
        byte[] buffer;
        //string[,] grid = Import("TextFile1.txt", out buffer);

        public void Start()
        {
            //string[,] grid = Levelreader.ReadFileToArray("TextFile1.txt", out byte[] buffer);
            //string[,] grid = Import("TextFile1.txt", out buffer);
            
            TheWorld = new Maze();
            ThePlayer = new Player(1, 1);
            buffer = TheWorld.Import("TextFile1.txt");
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
            FastDraw(buffer);
            while (true)
            {
                
                ThePlayer.DrawModel();
                PlayerInput();
            }
        }

        public void FastDraw(byte[] buffer)
        {
            using (var stdout = Console.OpenStandardOutput(buffer.Length))
            {
                // fill

                stdout.Write(buffer, 3, buffer.Length - 3);
                // rinse and repeat
            }

        }
    }
}
