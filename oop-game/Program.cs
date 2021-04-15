using System;
using System.Collections.Generic;
using System.Threading;

namespace oop_game
{
    class Program
    {
        static GameSession _gameSession;
        static byte[] buffer;
        static void Main(string[] args)
        {
            _gameSession = new GameSession();
            buffer = _gameSession.currentMaze.Buffer;
            Console.CursorVisible = true;
            
            Menu();
            //GameLoop();
        }


        public static void Menu()
        {
            bool validChoice;
            int selection;
            Console.ForegroundColor = ConsoleColor.Green;
            
            do
            {
                Console.Clear();
                Console.WriteLine($"Välkommen {Environment.UserName}!");
                Console.WriteLine("[1] Spela ");
                Console.WriteLine("[2] Instruktioner ");
                Console.WriteLine("[3] Avsluta");
                Console.Write("Välj [1..3] :");

                validChoice = int.TryParse(Console.ReadLine(), out selection);
                if (!validChoice)
                {
                    Console.Beep(1000, 50);
                }
                    
            } while (!validChoice);

            switch(selection)
            {
                case 1:
                    GameLoop();
                    break;
                case 2:
                    ViewInstructions();
                    break;
                case 3:
                    Exit();
                    break;
                default:
                    Console.Beep(1000, 50);
                    Menu();
                    break;
            }
        }
        public static void ViewInstructions()
        {
            Console.WriteLine("Instruktioner...");
            Console.ReadKey();
            Menu();
        }

        public static void Exit()
        {
            Console.WriteLine("Tryck på en knapp");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void PlayerInput()
        {
            ConsoleKeyInfo keypress = Console.ReadKey(true);
            ConsoleKey key = keypress.Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    ErasePlayer();
                    _gameSession.Move(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    ErasePlayer();
                    _gameSession.Move(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    ErasePlayer();
                    _gameSession.Move(-1, 0); 
                    break;
                case ConsoleKey.RightArrow:
                    ErasePlayer();
                    _gameSession.Move(1, 0);
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                        Menu();
                        break;
            }
        }
        private static void GameLoop()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Console.ResetColor();
            FastDraw(buffer);
            while (true)
            {
                DrawPlayer();
                DrawEnemy();
                PlayerInput();
            }
        }
        public static void DrawEnemy()
        {
            foreach(Enemy enemy in _gameSession.enemies)
            {
                //Console.WriteLine(_gameSession.enemies.GetType().GetProperty(_gameSession.enemies.ToString())); 
                Console.ForegroundColor = enemy.Color;
                Console.SetCursorPosition(enemy.X, enemy.Y);
                Console.Write(enemy.Model);
                Console.ResetColor();
            }           
        }
        public static void DrawPlayer()
        {
            Console.ForegroundColor = _gameSession.currentPlayer.PlayerColor;
            Console.SetCursorPosition(_gameSession.currentPlayer.X, _gameSession.currentPlayer.Y);
            Console.Write(_gameSession.currentPlayer.PlayerModel);
            Console.ResetColor();
        }
        public static void ErasePlayer()
        {
            Console.ForegroundColor = _gameSession.currentPlayer.PlayerColor;
            Console.SetCursorPosition(_gameSession.currentPlayer.X, _gameSession.currentPlayer.Y);
            Console.Write(" ");
            Console.ResetColor();
        }
        public static void FastDraw(byte[] buffer)
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
