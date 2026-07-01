namespace MazeRun
{
    public sealed class DiamondCounter
    {
        public int Collected { get; private set; }
        public int Total { get; }
        public bool IsComplete => Collected == Total;

        public DiamondCounter(int total) => Total = total;

        public void Collect() => Collected++;
    }
}
