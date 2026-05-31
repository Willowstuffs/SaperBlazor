using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Shared.Models
{
    //Klasa abstrakcyjna do planszy gry
    public abstract class BoardBase
    {
        public abstract int Width { get; protected set; }
        public abstract int Height { get; protected set; }
        public abstract int Mines { get; protected set; }

        public abstract void Reveal(int x, int y);
    }
}

