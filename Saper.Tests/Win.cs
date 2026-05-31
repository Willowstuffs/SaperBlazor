using Saper.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Tests
{
    public class Win
    {
        //Testy sprawdzajace czy gra konczy sie wygrana po odkryciu wszystkich komorek bez min
        [Fact]
        public void Test1()
        {
            var board = new GameBoard(3, 3, 1);
            board.Reveal(0, 0); // inicjalizacja min

            // Odkrywamy wszystkie nie-miny
            foreach (var cell in board.Cells)
            {
                if (!cell.HasMine)
                    board.Reveal(cell.X, cell.Y);
            }

            Assert.True(board.IsWin);
            Assert.False(board.IsGameOver);
        }

        [Fact]
        public void Test2()
        {
            var board = new GameBoard(5, 10, 15);
            board.Reveal(0, 0); // inicjalizacja min

            // Odkrywamy wszystkie nie-miny
            foreach (var cell in board.Cells)
            {
                if (!cell.HasMine)
                    board.Reveal(cell.X, cell.Y);
            }

            Assert.True(board.IsWin);
            Assert.False(board.IsGameOver);
        }

        [Fact]
        public void Test3()
        {
            var board = new GameBoard(30, 30, 100);
            board.Reveal(0, 0); // inicjalizacja min

            // Odkrywamy wszystkie nie-miny
            foreach (var cell in board.Cells)
            {
                if (!cell.HasMine)
                    board.Reveal(cell.X, cell.Y);
            }

            Assert.True(board.IsWin);
            Assert.False(board.IsGameOver);
        }
    }
}
