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
        public ASCIIModel currentMaze;
        public Player currentPlayer;
        public Enemy enemy1;
        public Enemy enemy2;
        public List<Enemy> enemies;

        public GameSession()
        {
            currentMaze = new ASCIIModel("ASCII/Level1.txt");
            currentPlayer = new Player(1, 1);
            enemy1 = new Enemy(2, 10);
            enemy2 = new Enemy(8, 4);
            enemies = new List<Enemy>();
            enemies.Add(enemy2);
            enemies.Add(enemy1);
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
            if (IsEnemy(x, y) != null)
            {
                //FIGHT
                Fighting(IsEnemy(x, y));
                currentMaze.Grid[y, x] = " ";
                
            }
            
        }

        public void Fighting(Enemy enemyToFight)
        {
           Fight currentFight = new Fight(currentPlayer, enemyToFight);
            while (currentFight.fightDone == false)
            {
                currentFight.TakeTurn();
            }
            if (currentPlayer.HitPoints > 0)
            {
                enemies.Remove(enemyToFight);
            }


        }

        public bool IsValidMove(int x, int y)
        {
            //check if new position is outside of level
           
            //returns true if new move goes to an empty space
            return currentMaze.Grid[y, x] == " ";
        }
        public Enemy IsEnemy(int x, int y)
        {
            //if (currentMaze.Grid[y, x] == "X")
            //{
            //    return true;
            //}
            
            foreach (Enemy enemy in enemies)
            {
                if(enemy.X == x && enemy.Y == y)
                {
                    return enemy;
                }
            }
            return null;
        }
    }
}
