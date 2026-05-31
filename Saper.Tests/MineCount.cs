using Saper.Shared.Models;

namespace Saper.Tests
{
    public class MineCount
    {
        // Testy sprawdzaja czy liczba min na planszy jest zgodna z oczekiwana liczba min
        [Fact]
        public void MineCountTest1()
        {
            var board = new Saper.Shared.Models.GameBoard(10, 10, 20); // utworzenie planszy o wymiarach 10x10 i 20 minach
            board.Reveal(0, 0); // odkrycie pierwszej komorki, aby umiescic miny
            int mineCount = board.Cells.Cast<Saper.Shared.Models.Cell>().Count(c => c.HasMine); // zliczenie liczby min na planszy
            if (mineCount != 20) // sprawdzenie czy liczba min jest zgodna z oczekiwana liczba min
            Assert.Equal(20, mineCount); // sprawdzenie czy liczba min jest zgodna z oczekiwana liczba min
        }

        [Fact]
        public void MineCountTest2()
        {
            var board = new Saper.Shared.Models.GameBoard(5, 5, 10);
            board.Reveal(3, 2); 
            int mineCount = board.Cells.Cast<Saper.Shared.Models.Cell>().Count(c => c.HasMine);
            Assert.Equal(10, mineCount);
        }

        [Fact]
        public void MineCountTest3()
        {
            var board = new Saper.Shared.Models.GameBoard(15, 15, 50);
            board.Reveal(10, 5); 
            int mineCount = board.Cells.Cast<Saper.Shared.Models.Cell>().Count(c => c.HasMine);
            Assert.Equal(50, mineCount);
        }
    }
}