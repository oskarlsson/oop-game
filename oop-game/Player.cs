using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string PlayerModel;
        public ConsoleColor PlayerColor;

        public Player(int startX, int startY)
        {
            X = startX;
            Y = startY;
            PlayerModel = "O";
            PlayerColor = ConsoleColor.Green;
        }
    }
}
