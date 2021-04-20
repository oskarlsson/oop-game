using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool isAlive;
        //public int health { get; set; }
        private int _attackDamage;
        public int AttackDamage
        {
            get
            {
                int dmg = 0;
                foreach (Item item in inventory)
                {
                    switch (item)
                    {
                        case Weapon wep:
                            dmg += wep.AttackDamage;
                            break;
                    }
                }
                //Add weaponstats and potioneffects to this
                return _attackDamage + dmg;
            }
        }
        private int _hitPoints;

        
        public int HitPoints
        {
            set
            {
                _hitPoints = value;
                if (_hitPoints <= 0)
                {
                    isAlive = false;
                }
            }
            get
            {
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
                _experiencePoints = value;
                if (_experiencePoints >= 100)
                {
                    LevelUp();
                    
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
            isAlive = true;

            // Add starting weapon and potion
            inventory = new List<Item>
            {
                new Weapon(5),
                new Potion(3,7)
            };
            //health = 100;
        }
        private void LevelUp()
        {
            level++;
            _attackDamage += 3;
            _hitPoints += 10;
            _experiencePoints -= 100;

        }

        public int Attack()
        {
            return AttackDamage;
        }
        
        public void DrinkPotion(Potion pot)
        {          
            if (pot.HealEffect > 0)
            {
                _hitPoints += pot.HealEffect;                
            }
            if (pot.AttackEffect > 0)
            {
                _attackDamage += pot.AttackEffect;
            }

            inventory.Remove(pot);
        }
    }
}
