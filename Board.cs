using Microsoft.AspNetCore.Components;
using System;

namespace BlazorWASM

{
    public class Board : ComponentBase
    {
        public Action<int, int, string>? OnCellUpdated { get; set; }
        public int Size { get; private set; }
        public Cell[,] Grid { get; private set; }
        private int Difficulty;

        public Board(int size, Action<int, int, string>? onCellUpdated = null)
        {
            Size = size;
            Grid = new Cell[size, size];
            OnCellUpdated = onCellUpdated;

            // Initialize cells
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Grid[row, col] = new Cell();
                }
            }
        }

        public void SetDifficulty(int difficulty)
        {
            if (difficulty >= 0 && difficulty <= 100)
            {
                Difficulty = difficulty;
            }
            else
            {
                Difficulty = 10;
            }
        }

        public void SetupLiveNeighbors()
        {
            Random rand = new Random();
            int count = 0;
            while (count < Size * Difficulty / 100)
            {
                int row = rand.Next(0, Size);
                int col = rand.Next(0, Size);

                if (!Grid[row, col].GetLive())
                {
                    Grid[row, col].SetLive(true);
                    count++;
                }
            }
        }

        public void CalculateLiveNeighbors()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    int liveNeighbors = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int newRow = row + i;
                            int newCol = col + j;
                            if (newRow >= 0 && newRow < Size && newCol >= 0 && newCol < Size)
                            {
                                if (Grid[newRow, newCol].GetLive())
                                {
                                    liveNeighbors++;
                                }
                            }
                        }
                    }
                    if (Grid[row, col].GetLive())
                    {
                        liveNeighbors--;
                    }
                    Grid[row, col].SetLiveNeighbors(liveNeighbors);
                    //Console.WriteLine($"Live neighbors for cell ({row}, {col}): {liveNeighbors}");
                }
            }
        }

        public void FloodFill(int row, int col)
        {
            if (row < 0 || row >= Size || col < 0 || col >= Size)
            {
                return;
            }

            Cell cell = Grid[row, col];

            if (cell.Visited || cell.Live)
            {
                return;
            }

            cell.SetVisited(true);
            OnCellUpdated?.Invoke(row, col, "🟩"); // green square emoji for visited cells

            if (cell.LiveNeighbors > 0)
            {
                return;
            }

            FloodFill(row - 1, col);
            FloodFill(row + 1, col);
            FloodFill(row, col - 1);
            FloodFill(row, col + 1);
            FloodFill(row - 1, col - 1);
            FloodFill(row - 1, col + 1);
            FloodFill(row + 1, col - 1);
            FloodFill(row + 1, col + 1);
        }

        public void FloodFillAll()
        {
            // Reset all cells to unvisited
            foreach (var cell in Grid)
            {
                cell.SetVisited(false);
            }

            // Flood fill from all cells with no live neighbors
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    var cell = Grid[row, col];
                    if (cell.LiveNeighbors == 0 && !cell.Visited)
                    {
                        FloodFill(row, col);
                    }
                }
            }
        }




    }

}
