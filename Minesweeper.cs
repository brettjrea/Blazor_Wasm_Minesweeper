using BlazorWASM;


namespace BlazorWASM
{
    public class MinesweeperGame
    {
        public Board Board { get; private set; }
        public bool IsGameOver { get; private set; }
        public bool IsGameWon { get; private set; }

        public MinesweeperGame(int size, int difficulty)
        {
            Board = new Board(size);
            Board.SetDifficulty(difficulty);
            Board.SetupLiveNeighbors();
            Board.CalculateLiveNeighbors();
        }

        public void ClickCell(int row, int column)
        {
            if (IsGameOver) return;

            Cell cell = Board.Grid[row, column];

            if (cell.Live)
            {
                IsGameOver = true;
                RevealAllCells();
                return;
            }

            cell.Visited = true;

            if (cell.LiveNeighbors == 0)
            {
                Board.FloodFill(row, column);
            }

            CheckIfGameWon();
        }

        private void RevealAllCells()
        {
            for (int row = 0; row < Board.Size; row++)
            {
                for (int column = 0; column < Board.Size; column++)
                {
                    Board.Grid[row, column].Visited = true;
                }
            }
        }

        private void CheckIfGameWon()
        {
            int notLiveLeftCount = 0;

            for (int row = 0; row < Board.Size; row++)
            {
                for (int column = 0; column < Board.Size; column++)
                {
                    if (!Board.Grid[row, column].Visited && !Board.Grid[row, column].Live)
                    {
                        notLiveLeftCount++;
                    }
                }
            }

            if (notLiveLeftCount == 0)
            {
                IsGameOver = true;
                IsGameWon = true;
            }
        }
    }
}
