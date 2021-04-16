using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    public class Potion : Item
    {
        public int HealEffect { get; set; }
        public int AttackEffect { get; set; }


        public Potion(int hpEffect, int atkEffect)
        {
            itemModel = "P";
            itemColor = ConsoleColor.Cyan;
            HealEffect = hpEffect;
            AttackEffect = atkEffect;
        }

        public override string ToString()
        {
            return $"Potion with +HP {HealEffect} and +ATK {AttackEffect}";
        }
    }
}
