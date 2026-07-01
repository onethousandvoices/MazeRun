using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MazeRun
{
    public sealed class GameResultView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _details;
        [SerializeField] private Button _restartButton;

        private void Awake() => _restartButton.onClick.AddListener(Restart);

        public void Hide() => this.SetActiveSafe(false);

        public void ShowVictory(int collected, int total)
        {
            _title.text = "Victory";
            _details.text = $"Diamonds: {collected} / {total}";
            this.SetActiveSafe(true);
        }

        public void ShowDefeat()
        {
            _title.text = "Defeat";
            _details.text = "";
            this.SetActiveSafe(true);
        }

        private static void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
