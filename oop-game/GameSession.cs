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
        public List<Item> drops;
        public List<string> eventLogs;
        public bool inFight;
        public Fight currentFight;

        public GameSession()
        {
            currentMaze = new ASCIIModel("ASCII/Level1.txt");
            currentPlayer = new Player(1, 1);

            // Add enemies to map
            enemies = new List<Enemy>()
            {
                new Enemy(2, 10),
                new Enemy(8, 4, new Potion(20, 1)),
                new Enemy(150, 20),
                new Enemy(72, 17, new Weapon(7)),
                new Enemy(21, 26)
            };

            // Add drops to map
            drops = new List<Item>()
            {
                new Potion(1, 1){ X = 5, Y = 5 },
                new Potion(1, 1){ X = 136, Y = 4 },
                new Weapon(9){ X = 164, Y = 18}
            };
            eventLogs = new List<string>();

            // Add random potion to map
            //drops.Add(new Potion(1, 1){ X = 5, Y = 5 });
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

            if (IsDrop(x, y) != null)
            {
                eventLogs.Add("Picked up a " + IsDrop(x, y));
                currentPlayer.inventory.Add(IsDrop(x, y));
                drops.Remove(IsDrop(x, y));
            }
            
        }

        public void Fighting(Enemy enemyToFight)
        {
            currentFight = new Fight(currentPlayer, enemyToFight);
            currentFight.fightlog += Fight_OnTurnTaken;
            currentFight.Win += Fight_OnWin;
            currentFight.Death += Fight_OnDeath;
            currentMaze = currentFight.fightScene;
            inFight = true;
            //while (currentFight.fightDone == false)
            //{
            //    currentFight.TakeTurn();
            //}
            //inFight = false;
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

        public Item IsDrop(int x, int y)
        {
            foreach (Item drop in drops)
            {
                if (drop.X == x && drop.Y == y)
                {
                    return drop;
                }
            }
            return null;
        }

        public void Fight_OnTurnTaken(object sender, string fightlog)
        {
            eventLogs.Add(fightlog);
        }
        public void Fight_OnWin(object sender, Enemy deadEnemy)
        {
            Fight fight = (Fight)sender;
            eventLogs.Add($"You defeated an enemy and gained {deadEnemy.experienceReward} experience");
            foreach (Item item in deadEnemy.Drops)
            {

                drops.Add(item);

            }
            currentPlayer.ExperiencePoints += deadEnemy.experienceReward;
            enemies.Remove(deadEnemy);
            currentMaze = new ASCIIModel("ASCII/Level1.txt");
            inFight = false;
        }
        public void Fight_OnDeath(object sender, EventArgs e)
        {

            eventLogs.Add($"You died                                     ");
            inFight = false;
        }
    }
}
