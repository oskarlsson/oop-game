﻿using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    class World
    {
        public string[,] Grid;
        private int Rows;
        private int Cols;

        public World(string[,] grid)
        {
            Grid = grid;
            Rows = Grid.GetLength(0);
            Cols = Grid.GetLength(1);
        }

        public void FastDraw(byte[] buffer)
        {
            using (var stdout = Console.OpenStandardOutput(Cols * Rows))
            {
                // fill

                stdout.Write(buffer, 3, buffer.Length-3);
                // rinse and repeat
            }

        }



        }

        public void Draw()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Cols; x++)
                {
                    string element = Grid[y, x];
                    Console.SetCursorPosition(x, y);
                    Console.Write(element);
                }
            }
        }
        public bool Walkable(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Cols || y >= Rows)
            {
                return false;
            }
            return Grid[y, x] == " "; 
        }
    }
}
