using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    public interface IView
    {
        public abstract byte[] Import(string filepath);


        //VOODOO
        public abstract bool Walkable(int x, int y);
    }
}
