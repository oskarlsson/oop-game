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
            GameLoop();
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
                //default:
                    //break;
            }
        }
        private static void GameLoop()
        {
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
