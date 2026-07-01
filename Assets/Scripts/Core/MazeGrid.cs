using UnityEngine;

namespace MazeRun
{
    public sealed class MazeGrid
    {
        private readonly MazeConfig _config;

        public int Width => _config.Width;
        public int Height => _config.Height;
        public float CellSize => _config.CellSize;

        public MazeGrid(MazeConfig config) => _config = config;

        public bool IsWall(Vector2Int cell) => _config.IsWall(cell);

        public Vector3 CellToWorld(Vector2Int cell)
        {
            float x = (cell.x - Width * 0.5f + 0.5f) * CellSize;
            float z = (Height * 0.5f - cell.y - 0.5f) * CellSize;
            return new(x, 0f, z);
        }

        public Vector2Int WorldToCell(Vector3 world)
        {
            int x = Mathf.RoundToInt(world.x / CellSize + Width * 0.5f - 0.5f);
            int y = Mathf.RoundToInt(Height * 0.5f - 0.5f - world.z / CellSize);
            return new(x, y);
        }

        public Vector2Int NextCell(Vector2Int from, Vector2Int to)
        {
            int x = to.x.CompareTo(from.x);
            int y = to.y.CompareTo(from.y);
            Vector2Int primary = Mathf.Abs(to.x - from.x) > Mathf.Abs(to.y - from.y)
                ? new(from.x + x, from.y)
                : new(from.x, from.y + y);
            Vector2Int secondary = primary.x == from.x
                ? new(from.x + x, from.y)
                : new(from.x, from.y + y);

            if (primary != from && !IsWall(primary))
            {
                return primary;
            }

            return secondary != from && !IsWall(secondary) ? secondary : from;
        }
    }
}
