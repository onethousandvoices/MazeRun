using UnityEngine;

namespace MazeRun
{
    public sealed class Enemy : MonoBehaviour
    {
        private EnemyConfig _config;
        private GameSession _session;
        private MazeGrid _grid;
        private Transform _player;
        private Vector2Int[] _patrolCells;
        private Vector2Int _moveCell;
        private int _patrolIndex;

        public void Initialize(EnemyConfig config, MazeGrid grid, Transform player, GameSession session, Vector2Int[] patrolCells)
        {
            _config = config;
            _grid = grid;
            _player = player;
            _session = session;
            _patrolCells = patrolCells;
            _moveCell = grid.WorldToCell(transform.position);
        }

        private void Update()
        {
            if (!_session.IsPlaying)
            {
                return;
            }

            var toPlayer = _player.position - transform.position;
            toPlayer.y = 0f;

            if (toPlayer.sqrMagnitude <= _config.CatchRadiusSqr)
            {
                _session.Lose();
                return;
            }

            if (toPlayer.sqrMagnitude <= _config.DetectRadiusSqr)
            {
                MoveToCell(_session.PlayerTrailTarget(_moveCell), _config.ChaseSpeed);
                return;
            }

            if (MoveToCell(_patrolCells[_patrolIndex], _config.PatrolSpeed))
            {
                _patrolIndex = (_patrolIndex + 1) % _patrolCells.Length;
            }
        }

        private bool MoveToCell(Vector2Int targetCell, float speed)
        {
            var target = _grid.CellToWorld(_moveCell);
            target.y = transform.position.y;

            if ((target - transform.position).sqrMagnitude < 0.04f)
            {
                if (_moveCell == targetCell)
                {
                    return true;
                }

                _moveCell = _grid.NextCell(_moveCell, targetCell);
                target = _grid.CellToWorld(_moveCell);
                target.y = transform.position.y;
            }

            Move(target, speed);
            return false;
        }

        private void Move(Vector3 target, float speed)
        {
            var position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            transform.position = position;
            var direction = target - position;

            if (direction.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
