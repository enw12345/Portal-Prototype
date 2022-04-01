using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public bool GameStart { get; private set; }

        public Canvas StartScreenUI;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);

            else
                Instance = this;

            GameStart = false;
            StartScreenUI.gameObject.SetActive(true);
        }

        private void LateUpdate()
        {
            if (!GameStart && !Input.anyKeyDown) return;
            StartGame();
            StartScreenUI.gameObject.SetActive(false);
        }

        private void StartGame()
        {
            GameStart = true;
        }
    }
}