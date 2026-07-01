using UnityEngine;
using UnityEngine.InputSystem;

namespace MazeRun
{
    public sealed class PlayerLook : MonoBehaviour
    {
        [SerializeField] private Transform _playerRoot;

        private PlayerConfig _config;
        private bool _skipDelta;
        private float _pitch;

        public void Initialize(PlayerConfig config)
        {
            _config = config;
            _skipDelta = true;
        }

        private void Update()
        {
            if (_skipDelta)
            {
                _skipDelta = false;
                return;
            }

            var delta = Mouse.current.delta.ReadValue() * _config.LookSensitivity;
            _pitch = Mathf.Clamp(_pitch - delta.y, -80f, 80f);
            transform.localEulerAngles = new(_pitch, 0f, 0f);
            _playerRoot.Rotate(Vector3.up * delta.x);
        }
    }
}
