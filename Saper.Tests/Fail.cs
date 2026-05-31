using Saper.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Tests
{
    public class Fail
    {
        //test sprawdzajacy czy gra konczy sie po kliknieciu w mine
        [Fact]
        public void Test1()
        {
            var board = new GameBoard(5, 5, 5);
            board.Reveal(0, 0); // pierwszy klik — inicjalizacja

            // Szukamy komórki z miną
            var mine = board.Cells.Cast<Cell>().First(c => c.HasMine); // znajdź pierwszą komórkę z miną

            board.Reveal(mine.X, mine.Y); // Klikamy w komórke z mina

            Assert.True(board.IsGameOver); // Sprawdzenie, czy gra jest zakończona
            Assert.Equal(CellState.Exploded, board.Cells[mine.X, mine.Y].State); // Sprawdzenie, czy mina zostala odkryta i wybuchla
        }

    }
}
