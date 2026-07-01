using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace MazeRun
{
    public sealed class UiRoot : MonoBehaviour
    {
        [SerializeField] private InputSystemUIInputModule _inputModule;

        private void Awake() => _inputModule.AssignDefaultActions();
    }
}
