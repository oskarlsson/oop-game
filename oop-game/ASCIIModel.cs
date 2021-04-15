using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace oop_game
{
    public class ASCIIModel
    {
        public byte[] Buffer;
        public string[,] Grid;
        public int rows;
        public int cols;

        public ASCIIModel(string filepath)
        {
            Buffer = File.ReadAllBytes(filepath);
            //reads the file into a string array and sets the width and height of the level
            string[] lines = File.ReadAllLines(filepath);
            string firstLine = lines[0];
            int row = lines.Length;
            int col = firstLine.Length;
            string[,] grid = new string[row, col];
            for (int y = 0; y < row; y++)
            {
                string line = lines[y];
                for (int x = 0; x < col; x++)
                {
                    char currentChar = line[x];
                    grid[y, x] = currentChar.ToString();
                }
            }
            Grid = grid;
            rows = Grid.GetLength(0);
            cols = Grid.GetLength(1);
        }
    }
}
