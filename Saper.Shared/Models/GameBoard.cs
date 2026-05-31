using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Saper.Shared.Models
{
    //Glowny komponent Sapera, czyli plansza gry
    public class GameBoard : BoardBase
    {
        public override int Width { get; protected set; } // szerokosc planszy. Protected set pozwala na ustawienie wartosci tylko w klasie GameBoard
        public override int Height { get; protected set; } // wysokosc planszy
        public override int Mines { get; protected set; } // liczba min na planszy
        public Cell[,] Cells { get; } // dwuwymiarowa tablica komorek
        public bool IsGameOver { get; private set; } = false; // czy gra jest zakonczona
        public bool IsWin =>
        !IsGameOver && Cells.Cast<Cell>().Count(c => !c.HasMine && c.State == CellState.Revealed) == Width * Height - Mines;
        //Wygrana nastepuje po odkryciu wszystkich pol niebedacych minami
        const int MinSize = 5;
        const int MaxSize = 30;
        private bool minesPlaced = false; //flaga do logiki pierwszego klikniecia

        private static readonly (int dx, int dy)[] Neighbors = {
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1),           (0, 1),
            (1, -1),  (1, 0),  (1, 1)
        }; // kierunki sasiadow wzgledem obecnej komorki

        public GameBoard(int width, int height, int mines) //konstruktor
        {
            Width = Math.Clamp(width, MinSize, MaxSize); //Clamp ogranicza wartosc width do zakresu MinSize - MaxSize
            Height = Math.Clamp(height, MinSize, MaxSize);
            int maxMines = Width * Height - 1; // co najmniej jedno pole musi być puste
            Mines = Math.Clamp(mines, 1, maxMines); // ograniczenie liczby min do maksymalnej mozliwej liczby

            Cells = new Cell[Width, Height]; // inicjalizacja dwuwymiarowej tablicy komorek

            // Inicjalizacja komorek planszy
            for (int x = 0; x < Width; x++) 
                for (int y = 0; y < Height; y++)
                    Cells[x, y] = new Cell { X = x, Y = y }; // tworzenie komorek planszy
        }
        //Rozmieszczenie min na planszy z uwzglednieniem pozycji pierwszej kliknietej komorki jako bezpiecznej
        private void PlaceMinesAvoiding(int avoidX, int avoidY)
        {
            var rand = new Random();
            int placed = 0;

            while (placed < Mines)
            {
                int x = rand.Next(Width);
                int y = rand.Next(Height);
                // Sprawdzenie czy komorka jest juz zajeta mina lub jest to komorka, ktora zostala kliknieta jako pierwsza
                if ((x == avoidX && y == avoidY) || Cells[x, y].HasMine) 
                    continue;

                Cells[x, y].HasMine = true;
                placed++;
            }
        }
        private void CalculateAdjacentMines() // Obliczanie liczby min w sasiednich komorkach
        {
            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    int count = 0;

                    foreach (var (dx, dy) in Neighbors) // Iteracja po wszystkich sasiadujacych komorkach
                    {
                        int nx = x + dx; // Nowa wspolrzedna X
                        int ny = y + dy; // Nowa wspolrzedna Y
                        if (nx >= 0 && ny >= 0 && nx < Width && ny < Height && Cells[nx, ny].HasMine) // Sprawdzenie czy nowa wspolrzedna jest w granicach planszy i czy komorka ma mine
                            count++;
                    }
                    Cells[x, y].AdjacentMines = count; // Ustawienie liczby min w poblizu komorki
                }
            }
        }
        public override void Reveal(int x, int y) // Odkrywanie komorki na planszy
        {
            if (IsGameOver || !InBounds(x, y)) return; // Sprawdzenie czy gra jest zakonczona lub czy wspolrzedne sa poza plansza

            if (!minesPlaced) //Sprawdzenie czy rozlozono miny
            { 
                PlaceMinesAvoiding(x, y); // Rozlozenie min z uwzglednieniem pozycji kliknietej komorki
                CalculateAdjacentMines(); // Obliczenie liczby min w poblizu komorek
                minesPlaced = true; // Ustawienie flagi, ze miny zostaly rozlozone
            }

            var cell = Cells[x, y]; 

            if (cell.State != CellState.Hidden) return; 

            if (cell.HasMine) 
            {
                cell.State = CellState.Exploded; // Kablooie
                IsGameOver = true; // Koniec gry
                RevealAllMines(); // Odkrywamy wszystkie miny
                return;
            }
            cell.State = CellState.Revealed; // Odkrywamy komorke

            if (cell.AdjacentMines == 0) // Jesli komorka nie ma min w poblizu
            {
                // Odkrywamy wszystkie sasiadujace komorki
                foreach (var (dx, dy) in Neighbors)
                {
                    Reveal(x + dx, y + dy); // Rekurencyjne odkrywanie sasiadow, przez ktore moze dojsc do odkrycia wiekszej liczby komorek
                }
            }
        }

        public void ToggleFlag(int x, int y)
        { // Zmiana stanu flagi na komorce
            if (!InBounds(x, y)) return;
            var cell = Cells[x, y];

            if (cell.State == CellState.Hidden)
            { // Dodajemy flage
                cell.State = CellState.Flagged;
            } else if (cell.State == CellState.Flagged)
            { // Usuwamy flage
                cell.State = CellState.Hidden;
            }
        }
        private void RevealAllMines() // Odkrywanie wszystkich min na planszy
        {
            foreach (var c in Cells)
            {
                if(c.HasMine && c.State == CellState.Hidden || c.State == CellState.Flagged) // Odkrywamy tylko te miny, ktore sa ukryte lub oznaczone flaga
                {
                    c.State = CellState.Revealed; // Odkrywamy wszystkie miny
                }
            }
        }
        // Sprawdzenie czy wspolrzedne sa w granicach planszy
        private bool InBounds(int x, int y) => x >= 0 && y >= 0 && x < Width && y < Height;
    }
}
