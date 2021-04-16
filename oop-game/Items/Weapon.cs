using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    public class Weapon : Item
    {
        public int AttackDamage { get; set; }

        public Weapon(int attackDamage)
        {
            itemModel = "W";
            itemColor = ConsoleColor.Magenta;
            AttackDamage = attackDamage;
        }
        public override string ToString()
        {
            return $"Weapon with +ATK {AttackDamage}";
        }
    }
}
