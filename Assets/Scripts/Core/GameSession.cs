using UnityEngine;

namespace MazeRun
{
    public sealed class GameSession : MonoBehaviour
    {
        [SerializeField] private MazeConfig _mazeConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private PrefabConfig _prefabs;

        private readonly Vector2Int[] _playerTrail = new Vector2Int[128];
        private DiamondCounter _diamonds;
        private GameHud _hud;
        private GameResultView _result;
        private MazeGrid _grid;
        private PlayerMovement _player;
        private Vector2Int _playerCell;
        private int _playerTrailCount;
        private int _playerTrailStart;
        private bool _ended;

        public bool IsPlaying => !_ended;

        private void Awake()
        {
            _grid = new(_mazeConfig);
            var mazeRoot = CreateRoot("Maze");
            var playerRoot = CreateRoot("Player");
            var exitRoot = CreateRoot("Exit");
            var diamondRoot = CreateRoot("Diamonds");
            var enemyRoot = CreateRoot("Enemies");
            var uiRoot = Instantiate(_prefabs.UiRootPrefab, transform);
            uiRoot.name = "UI";
            new MazeBuilder(_prefabs, _grid, mazeRoot).Build();
            _diamonds = new(_mazeConfig.DiamondCount);
            _hud = Instantiate(_prefabs.HudPrefab, uiRoot.transform);
            _result = Instantiate(_prefabs.ResultPrefab, uiRoot.transform);
            _result.Hide();
            _hud.Show(_diamonds.Collected, _diamonds.Total);

            var playerPosition = _grid.CellToWorld(_mazeConfig.PlayerCell);
            var playerLook = _grid.CellToWorld(_mazeConfig.PlayerCell + Vector2Int.right) - playerPosition;
            _player = Instantiate(_prefabs.PlayerPrefab, playerPosition, Quaternion.LookRotation(playerLook), playerRoot);
            _player.Initialize(_playerConfig);
            RecordPlayerCell();

            var exit = Instantiate(_prefabs.ExitPrefab, _grid.CellToWorld(_mazeConfig.ExitCell) + Vector3.up * 0.05f, Quaternion.identity, exitRoot);
            exit.localScale = new(_grid.CellSize * 0.8f, 0.1f, _grid.CellSize * 0.8f);

            var diamondCells = _mazeConfig.DiamondCells;
            int diamondOffset = Random.Range(0, diamondCells.Length);

            for (int i = 0; i < _mazeConfig.DiamondCount; i++)
            {
                var diamond = Instantiate(_prefabs.DiamondPrefab, _grid.CellToWorld(diamondCells[(diamondOffset + i) % diamondCells.Length]) + Vector3.up * 0.8f, Quaternion.identity, diamondRoot);
                diamond.Initialize(this);
            }

            var enemySpawns = _mazeConfig.EnemySpawns;

            for (int i = 0; i < enemySpawns.Length; i++)
            {
                var enemy = Instantiate(_prefabs.EnemyPrefab, _grid.CellToWorld(enemySpawns[i].Cell) + Vector3.up * 0.8f, Quaternion.identity, enemyRoot);
                enemy.Initialize(_enemyConfig, _grid, _player.transform, this, enemySpawns[i].PatrolCells);
            }

            LockCursor();
        }

        private void Update()
        {
            if (_ended)
            {
                return;
            }

            RecordPlayerCell();

            if (!_diamonds.IsComplete || _grid.WorldToCell(_player.transform.position) != _mazeConfig.ExitCell) return;
            End();
            _result.ShowVictory(_diamonds.Collected, _diamonds.Total);
        }

        public void Collect(Diamond diamond)
        {
            diamond.Hide();
            _diamonds.Collect();
            _hud.Show(_diamonds.Collected, _diamonds.Total);
        }

        public void Lose()
        {
            End();
            _result.ShowDefeat();
        }

        public Vector2Int PlayerTrailTarget(Vector2Int cell)
        {
            int index = 0;
            int distance = int.MaxValue;

            for (int i = _playerTrailCount - 1; i >= 0; i--)
            {
                var trailCell = PlayerTrailCell(i);

                if (trailCell == cell)
                {
                    return PlayerTrailCell(Mathf.Min(i + 1, _playerTrailCount - 1));
                }

                if (_grid.NextCell(cell, trailCell) == cell)
                {
                    continue;
                }

                int cellDistance = (trailCell - cell).sqrMagnitude;

                if (cellDistance >= distance) continue;
                distance = cellDistance;
                index = i;
            }

            return PlayerTrailCell(index);
        }

        private Transform CreateRoot(string rootName)
        {
            var root = new GameObject(rootName).transform;
            root.SetParent(transform, false);
            return root;
        }

        private void RecordPlayerCell()
        {
            var cell = _grid.WorldToCell(_player.transform.position);

            if (_playerTrailCount > 0 && cell == _playerCell)
            {
                return;
            }

            _playerCell = cell;

            if (_playerTrailCount < _playerTrail.Length)
            {
                _playerTrail[_playerTrailCount] = cell;
                _playerTrailCount++;
                return;
            }

            _playerTrail[_playerTrailStart] = cell;
            _playerTrailStart = (_playerTrailStart + 1) % _playerTrail.Length;
        }

        private Vector2Int PlayerTrailCell(int index) => _playerTrail[(_playerTrailStart + index) % _playerTrail.Length];

        private void End()
        {
            _ended = true;
            _player.SetPlaying(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private static void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
