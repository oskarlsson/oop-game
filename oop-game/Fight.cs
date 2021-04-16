using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    public class Fight
    {
        Player _player;
        Enemy _enemy;
        public event EventHandler<string> fightlog;
        public bool isMonsterDead
        {
            get
            {
                return _enemy.HitPoints <= 0;
            }
        }
        public bool fightDone;
        public ASCIIModel fightScene;
        public Fight(Player player, Enemy enemyToFight)
        {
            _player = player;
            _enemy = enemyToFight;
            fightDone = false;
            fightScene = new ASCIIModel("ASCII/FightScene.txt");
        }

        public void TakeTurn()
        {
            _enemy.HitPoints -= _player.Attack();
            OnTurnTaken($"You did {_player.Attack()} to the monster");
            if (_enemy.HitPoints <= 0)
            {
                fightDone = true;
                return;
                
            }
            _player.HitPoints -= _enemy.Attack();
            OnTurnTaken($"The monster did {_enemy.Attack()} to you");
            if (_player.HitPoints <= 0)
            {
                fightDone = true;
                OnTurnTaken("You died");
            }
        }
        
        protected virtual void OnTurnTaken(string fightdata)
        {
            EventHandler<string> handler = fightlog;
            handler.Invoke(this, fightdata);
        }

    }
}
