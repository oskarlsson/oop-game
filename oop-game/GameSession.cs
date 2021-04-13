using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    public class GameSession
    {
        public IView _currentView;
        public Player _currentPlayer;

        public GameSession()
        {
            _currentView = new Maze();
            _currentPlayer = new Player(1, 1);
        }

    }
}
