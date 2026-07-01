using UnityEngine;
using UnityEngine.InputSystem;

namespace MazeRun
{
    public sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private PlayerLook _look;

        private PlayerConfig _config;

        public void Initialize(PlayerConfig config)
        {
            _config = config;
            _controller.Move(Vector3.up * _controller.skinWidth);
            _look.Initialize(config);
        }

        public void SetPlaying(bool playing)
        {
            enabled = playing;
            _look.enabled = playing;
        }

        private void Update()
        {
            var keyboard = Keyboard.current;
            var move = Vector3.zero;

            if (keyboard.wKey.isPressed)
            {
                move += transform.forward;
            }

            if (keyboard.sKey.isPressed)
            {
                move -= transform.forward;
            }

            if (keyboard.dKey.isPressed)
            {
                move += transform.right;
            }

            if (keyboard.aKey.isPressed)
            {
                move -= transform.right;
            }

            if (move.sqrMagnitude > 1f)
            {
                move.Normalize();
            }

            float speed = keyboard.leftShiftKey.isPressed ? _config.SprintSpeed : _config.WalkSpeed;
            _controller.Move(move * (speed * Time.deltaTime));
        }
    }
}
