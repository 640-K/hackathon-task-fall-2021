using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Entities;

namespace UI {
    public class WinScreen : MonoBehaviour
    {
        private const string StartSceneName = "MainScene";

        public GameObject winScreenCanvas;

        void Start()
        {
            SetActiveChildren(false);
            GameplayManager.instance.onWin.AddListener(WinListener);
        }

        void WinListener()
        {
            SetActiveChildren(true);
        }

        void ReturnToMainMenu()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                // Restart the game
                SceneManager.LoadScene(StartSceneName);
            }
        }

        private void SetActiveChildren(bool active)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(active);
            }
        }

    }
}
