using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    /// <summary>
    /// Holds and creates the objects relating to the game
    /// Contains the game logic
    /// </summary>
    public class GameSession
    {
        public Maze currentMaze;
        public Player currentPlayer;

        public GameSession()
        {
            currentMaze = new Maze("Level1.txt");
            currentPlayer = new Player(1, 1);
        }
        public void Move(int x, int y)
        {
            x += currentPlayer.X;
            y += currentPlayer.Y;
            
            if (IsValidMove(x, y))
            {
                currentPlayer.X = x;
                currentPlayer.Y = y;
                
            }
            
        }

        public bool IsValidMove(int x, int y)
        {
            //check if new position is outside of level
            if (x < 0 || y < 0 || x >= currentMaze.cols || y >= currentMaze.rows)
            {
                return false;
            }
            //returns true if new move goes to an empty space
            return currentMaze.Grid[y, x] == " ";
        }

    }
}
