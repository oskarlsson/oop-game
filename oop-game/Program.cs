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

                    DrawModel(_gameSession.currentPlayer.PlayerColor, _gameSession.currentPlayer.X, _gameSession.currentPlayer.Y);
                    _gameSession.Move(_gameSession.currentPlayer.X, _gameSession.currentPlayer.Y - 1);


                    break;
                case ConsoleKey.DownArrow:
                    DrawModel(_gameSession.currentPlayer.PlayerColor, _gameSession.currentPlayer.X, _gameSession.currentPlayer.Y);
                    _gameSession.Move(_gameSession.currentPlayer.X, _gameSession.currentPlayer.Y + 1);

                    break;
                case ConsoleKey.LeftArrow:
                 
                    DrawModel(_gameSession.currentPlayer.PlayerColor, _gameSession.currentPlayer.X, _gameSession.currentPlayer.Y);
                    _gameSession.Move(_gameSession.currentPlayer.X -1, _gameSession.currentPlayer.Y );

                    break;
                case ConsoleKey.RightArrow:
             
                    DrawModel(_gameSession.currentPlayer.PlayerColor, _gameSession.currentPlayer.X, _gameSession.currentPlayer.Y);
                    _gameSession.Move(_gameSession.currentPlayer.X + 1, _gameSession.currentPlayer.Y);
                    
                    break;
                default:
                    break;
            }
        }
        private static void GameLoop()
        {
            FastDraw(buffer);
            while (true)
            {

                DrawModel(_gameSession.currentPlayer.PlayerColor, _gameSession.currentPlayer.X, _gameSession.currentPlayer.Y,
                    _gameSession.currentPlayer.PlayerModel);
                PlayerInput();
            }
        }

        public static void DrawModel(ConsoleColor modelColor, int xPos, int yPos, string modelChar = " ")
        {
            Console.ForegroundColor = modelColor;
            Console.SetCursorPosition(xPos, yPos);
            Console.Write(modelChar);
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
