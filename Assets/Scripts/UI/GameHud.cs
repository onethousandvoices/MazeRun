using TMPro;
using UnityEngine;

namespace MazeRun
{
    public sealed class GameHud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _counter;

        public void Show(int collected, int total) => _counter.text = $"Diamonds: {collected} / {total}";
    }
}
