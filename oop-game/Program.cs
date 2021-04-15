using System;
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
            Console.CursorVisible = false;
            menu();
        }

        public static void menu()
        {
            Console.Clear();
            int x = 5, y = 5; // Position of the menu on the screen
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("     Welcome {0} ", Environment.UserName);
            Console.SetCursorPosition(x, y);
            Console.WriteLine("Spela ");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("Instruktioner");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("Avsluta");
            Console.CursorVisible = false;
            do // Main loop for the menu. It continues until the user select 'AVSLUTA'
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(x - 3, y);
                Console.WriteLine("■■"); // Menu pointer
                ConsoleKeyInfo keypress = Console.ReadKey(true);
                ConsoleKey key = keypress.Key;
                switch (key)
                {
                    case ConsoleKey.DownArrow:
                        if (y == 7) // Last option of the menu
                        {
                            Console.SetCursorPosition(x - 3, y);
                            Console.WriteLine("  ");
                            y = 5;
                            Console.SetCursorPosition(x - 3, y);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("■■");
                        }
                        else
                        {
                            Console.SetCursorPosition(x - 3, y);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine("  ");
                            y += 1;
                            Console.SetCursorPosition(x - 3, y);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("■■");
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (y == 5)  // First option of the menu
                        {
                            Console.SetCursorPosition(x - 3, y);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine("  ");
                            y = 7;
                            Console.SetCursorPosition(x - 3, y);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("■■");
                        }
                        else
                        {
                            Console.SetCursorPosition(x - 3, y);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.WriteLine("  ");
                            y -= 1;
                            Console.SetCursorPosition(x - 3, y);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("■■");
                        }
                        break;

                    case ConsoleKey.Enter:
                        if (y == 5)
                        {
                            Console.ResetColor();
                            GameLoop();
                        }
                        else
                        if (y == 6)
                        {
                            viewInstruction(); // To do 
                            Console.ReadKey();
                            menu();
                        }

                        if (y == 7)
                        {
                            // The game is stopped and ended by user
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Tryck på en knapp!");
                            exit();
                        }

                        break;
                    default:
                        break;
                }
                Console.CursorVisible = false;
            } while (true);
        }
        public static void viewInstruction()
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Instruktioner...");
            Console.ReadKey();
        }

        public static void exit()
        {
            Console.ResetColor();
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
                    menu();
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
                PlayerInput();
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

