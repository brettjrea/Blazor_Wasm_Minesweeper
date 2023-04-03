namespace BlazorWASM
{
    public class Cell
    {
        public bool Visited { get; set; }
        public bool Live { get; set; }
        public int LiveNeighbors { get; set; }

        public Cell()
        {
            Visited = false;
            Live = false;
            LiveNeighbors = 0;
        }
        public bool IsFlagged { get; set; }
        public void SetVisited(bool visited)
        {
            Visited = visited;
          
        }


        public void SetLive(bool live)
        {
            Live = live;
        }

        public void SetLiveNeighbors(int liveNeighbors)
        {
            LiveNeighbors = liveNeighbors;
        }

        public bool GetVisited()
        {
            return Visited;
        }

        public bool GetLive()
        {
            return Live;
        }

        public int GetLiveNeighbors()
        {
            return LiveNeighbors;
        }
    }
}
