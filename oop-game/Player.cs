using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        private string PlayerModel;
        private ConsoleColor PlayerColor;

        public Player(int startX, int startY)
        {
            X = startX;
            Y = startY;
            PlayerModel = "O";
            PlayerColor = ConsoleColor.Green;
        }

        public void DrawModel()
        {
            Console.ForegroundColor = PlayerColor;
            Console.SetCursorPosition(X, Y);
            Console.Write(PlayerModel);
            Console.ResetColor();
        }
        public void DrawModel(char x)
        {
            Console.ForegroundColor = PlayerColor;
            Console.SetCursorPosition(X, Y);
            Console.Write(x);
            Console.ResetColor();
        }
    }
}
