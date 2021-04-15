using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    public class Enemy
    {
        public int level;
        public string Model;
        public ConsoleColor Color;
        public int X { get; set; }
        public int Y { get; set; }
        private int _attackDamage;
        public int AttackDamage
        {
            get
            {
                //Add weaponstats and potioneffects to this
                return _attackDamage;
            }
        }
        private int _hitPoints;
        public int HitPoints
        {
            get
            {
                //Add item effects to this
                return _hitPoints;
            }
        }
        //public List<Item> inventory;
        public Enemy(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Model = "X";
            Color = ConsoleColor.Red;
            level = 1;
            _hitPoints = 20;
            _attackDamage = 5;
        }

    }
}
