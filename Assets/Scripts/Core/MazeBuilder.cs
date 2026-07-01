using UnityEngine;

namespace MazeRun
{
    public sealed class MazeBuilder
    {
        private const float FLOOR_HEIGHT = 0.1f;

        private readonly PrefabConfig _prefabs;
        private readonly MazeGrid _grid;
        private readonly Transform _root;

        public MazeBuilder(PrefabConfig prefabs, MazeGrid grid, Transform root)
        {
            _prefabs = prefabs;
            _grid = grid;
            _root = root;
        }

        public void Build()
        {
            for (int y = 0; y < _grid.Height; y++)
            {
                for (int x = 0; x < _grid.Width; x++)
                {
                    Vector2Int cell = new(x, y);
                    SpawnFloor(cell);

                    if (_grid.IsWall(cell))
                    {
                        SpawnWall(cell);
                    }
                }
            }
        }

        private void SpawnFloor(Vector2Int cell)
        {
            var floor = Object.Instantiate(_prefabs.FloorPrefab, _grid.CellToWorld(cell) - Vector3.up * FLOOR_HEIGHT * 0.5f, Quaternion.identity, _root);
            floor.localScale = new(_grid.CellSize, FLOOR_HEIGHT, _grid.CellSize);
        }

        private void SpawnWall(Vector2Int cell)
        {
            var wall = Object.Instantiate(_prefabs.WallPrefab, _grid.CellToWorld(cell) + Vector3.up * 1.25f, Quaternion.identity, _root);
            wall.localScale = new(_grid.CellSize, 2.5f, _grid.CellSize);
        }
    }
}
