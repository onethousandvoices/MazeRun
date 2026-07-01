using UnityEngine;

namespace MazeRun
{
    [CreateAssetMenu(menuName = "MazeRun/Enemy Config")]
    public sealed class EnemyConfig : ScriptableObject
    {
        [SerializeField] private float _patrolSpeed = 2f;
        [SerializeField] private float _chaseSpeed = 4f;
        [SerializeField] private float _detectRadius = 5f;
        [SerializeField] private float _catchRadius = 0.8f;

        public float PatrolSpeed => _patrolSpeed;
        public float ChaseSpeed => _chaseSpeed;
        public float DetectRadiusSqr => _detectRadius * _detectRadius;
        public float CatchRadiusSqr => _catchRadius * _catchRadius;
    }
}
