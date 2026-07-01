using UnityEngine;

namespace MazeRun
{
    [CreateAssetMenu(menuName = "MazeRun/Prefab Config")]
    public sealed class PrefabConfig : ScriptableObject
    {
        [SerializeField] private Transform _floorPrefab;
        [SerializeField] private Transform _wallPrefab;
        [SerializeField] private Transform _exitPrefab;
        [SerializeField] private PlayerMovement _playerPrefab;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Diamond _diamondPrefab;
        [SerializeField] private UiRoot _uiRootPrefab;
        [SerializeField] private GameHud _hudPrefab;
        [SerializeField] private GameResultView _resultPrefab;

        public Transform FloorPrefab => _floorPrefab;
        public Transform WallPrefab => _wallPrefab;
        public Transform ExitPrefab => _exitPrefab;
        public PlayerMovement PlayerPrefab => _playerPrefab;
        public Enemy EnemyPrefab => _enemyPrefab;
        public Diamond DiamondPrefab => _diamondPrefab;
        public UiRoot UiRootPrefab => _uiRootPrefab;
        public GameHud HudPrefab => _hudPrefab;
        public GameResultView ResultPrefab => _resultPrefab;
    }
}
