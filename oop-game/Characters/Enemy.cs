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
            set
            {
                _hitPoints = value;
            }
            get
            {
                //Add item effects to this
                return _hitPoints;
            }
        }
        public List<Item> Drops;
        public Enemy(int startX, int startY, Item drop = null)
        {
            Random testRNG = new Random();
            X = startX;
            Y = startY;
            Model = "X";
            Color = ConsoleColor.Red;
            level = 1;
            _hitPoints = level * testRNG.Next(5,20);
            _attackDamage = level * 20;
            Drops = new List<Item>();
            if(drop != null)
            {
                drop.X = startX;
                drop.Y = startY;
                Drops.Add(drop);
            }
 
 
            }

        public int Attack()
        {
            return AttackDamage;
        }

    }
}
