using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    class Fight
    {
        Player _player;
        Enemy _enemy;
        public bool fightDone;
        public Fight(Player player, Enemy enemyToFight)
        {
            _player = player;
            _enemy = enemyToFight;
            fightDone = false;
        }

        public void TakeTurn()
        {
            _enemy.HitPoints -= _player.Attack();
            if (_enemy.HitPoints <= 0)
            {
                fightDone = true;
                return;
                
            }
            _player.HitPoints -= _enemy.Attack();
            if (_player.HitPoints <= 0)
            {
                fightDone = true;

            }
        }


    }
}
