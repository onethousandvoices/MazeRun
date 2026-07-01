using UnityEngine;

namespace MazeRun
{
    public static class GameObjectExtensions
    {
        public static void SetActiveSafe(this Component component, bool active)
        {
            if (component && component.gameObject.activeSelf != active)
            {
                component.gameObject.SetActive(active);
            }
        }
    }
}
