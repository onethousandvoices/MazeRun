using UnityEngine;

namespace MazeRun
{
    public sealed class Diamond : MonoBehaviour
    {
        [SerializeField] private float _turnSpeed = 120f;

        private GameSession _session;

        public void Initialize(GameSession session) => _session = session;

        public void Hide() => this.SetActiveSafe(false);

        private void Update() => transform.Rotate(0f, _turnSpeed * Time.deltaTime, 0f, Space.World);

        private void OnTriggerEnter(Collider _) => _session.Collect(this);
    }
}
