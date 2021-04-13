using System;
using System.Collections.Generic;
using System.Text;

namespace oop_game
{
    interface IView
    {
        public abstract byte[] Import(string filepath);
    }
}
