using UnityEngine;

namespace MazeRun
{
    [CreateAssetMenu(menuName = "MazeRun/Player Config")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _walkSpeed = 4f;
        [SerializeField] private float _sprintSpeed = 7f;
        [SerializeField] private float _lookSensitivity = 0.1f;

        public float WalkSpeed => _walkSpeed;
        public float SprintSpeed => _sprintSpeed;
        public float LookSensitivity => _lookSensitivity;
    }
}
