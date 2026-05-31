using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//kod pojedynczej komorki w saperze

namespace Saper.Shared.Models
{
    public enum CellState
    {
        Hidden,
        Revealed,
        Flagged,
        Exploded
    }
    public class Cell
    {
        public int X { get; set; } // wspolrzedna X komorki
        public int Y { get; set; } // wspolrzedna Y komorki
        public bool HasMine { get; set; } // czy komorka zawiera mine
        public int AdjacentMines { get; set; } // liczba min w poblizu komorki
        public CellState State { get; set; } = CellState.Hidden; // stan komorki: bazowo ukryta
    }
}
