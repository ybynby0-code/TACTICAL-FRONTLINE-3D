using UnityEngine;

namespace TacticalFrontline.Core
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private MatchManager matchManager;

        private void Awake()
        {
            Application.targetFrameRate = 120;
            QualitySettings.vSyncCount = 0;
            if (matchManager == null)
                matchManager = FindObjectOfType<MatchManager>();
            if (matchManager == null)
                Debug.LogWarning("MatchManager is not present in this scene.");
        }
    }
}
