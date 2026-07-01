using System;
using UnityEngine;

namespace MazeRun
{
    [CreateAssetMenu(menuName = "MazeRun/Maze Config")]
    public sealed class MazeConfig : ScriptableObject
    {
        [SerializeField] private float _cellSize = 3f;
        [SerializeField] private string[] _rows;
        [SerializeField] private Vector2Int _playerCell;
        [SerializeField] private Vector2Int _exitCell;
        [SerializeField] private int _diamondCount;
        [SerializeField] private Vector2Int[] _diamondCells;
        [SerializeField] private EnemySpawn[] _enemySpawns;

        public float CellSize => _cellSize;
        public int Width => _rows[0].Length;
        public int Height => _rows.Length;
        public Vector2Int PlayerCell => _playerCell;
        public Vector2Int ExitCell => _exitCell;
        public int DiamondCount => _diamondCount;
        public Vector2Int[] DiamondCells => _diamondCells;
        public EnemySpawn[] EnemySpawns => _enemySpawns;

        public bool IsWall(Vector2Int cell) => _rows[cell.y][cell.x] == '#';
    }

    [Serializable]
    public sealed class EnemySpawn
    {
        [SerializeField] private Vector2Int _cell;
        [SerializeField] private Vector2Int[] _patrolCells;

        public Vector2Int Cell => _cell;
        public Vector2Int[] PatrolCells => _patrolCells;
    }
}
