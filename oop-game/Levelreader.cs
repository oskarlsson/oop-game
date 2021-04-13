using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace oop_game
{
    class Levelreader
    {
        public static string[,] ReadFileToArray(string filepath)
        {
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
            return grid;
        }

        public static byte[] ReadFileToByteArray(string filepath)
        {
            return File.ReadAllBytes(filepath);

        }

    }
}
