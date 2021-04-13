using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace oop_game
{
    class Maze : IView
    {
        public string[,] Grid;
        private int Rows;
        private int Cols;

        public Maze()
        {
            //Grid = grid;
            //Rows = Grid.GetLength(0);
            //Cols = Grid.GetLength(1);
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

        public byte[] Import(string filepath)
        {
            byte[] buffer = File.ReadAllBytes(filepath);
            string[] lines = File.ReadAllLines(filepath);
            string firstLine = lines[0];
            int rows = lines.Length;
            int cols = firstLine.Length;
            string[,] grid = new string[rows, cols];
            for (int y = 0; y < rows; y++)
            {
                string line = lines[y];
                for (int x = 0; x < cols; x++)
                {
                    char currentChar = line[x];
                    grid[y, x] = currentChar.ToString();
                }
            }
            Grid = grid;
            Rows = Grid.GetLength(0);
            Cols = Grid.GetLength(1);
            return buffer;
        }
    }
}
