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
            AttackDamage = attackDamage;
        }

        //public override string ToString()
        //{
        //    return $"Weapon - AttackDamage: {AttackDamage}";
        //}
    }
}
