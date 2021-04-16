using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        //public int health { get; set; }
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
        private int _experiencePoints;
        public int ExperiencePoints
        {
            get
            {
                return _experiencePoints;
            }
            set
            {
                if (value + _experiencePoints >= 100)
                {
                    _experiencePoints = value + _experiencePoints - 100;
                    LevelUp();
                }
                else
                {
                    _experiencePoints += value;
                }
               
            }
        }
        public int level;
        public string PlayerModel;
        public ConsoleColor PlayerColor;
        public List<Item> inventory;
       
        public Player(int startX, int startY)
        {
            X = startX;
            Y = startY;
            PlayerModel = "O";
            PlayerColor = ConsoleColor.Cyan;
            level = 1;
            ExperiencePoints = 0;
            _hitPoints = 50;
            _attackDamage = 10;

            // Add starting weapon and potion
            inventory = new List<Item>
            {
                new Weapon(5),
                //new Potion(3,7)
            };
            //health = 100;
        }
        private void LevelUp()
        {
            level++;
            _attackDamage += 3;
            _hitPoints += 10;
        }

        public int Attack()
        {
            return AttackDamage;
        }
        
    }
}
