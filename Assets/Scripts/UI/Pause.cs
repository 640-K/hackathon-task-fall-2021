using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class Pause : MonoBehaviour
    {
        public GameObject healthBarCanvas;
        public GameObject auraBarCanvas;

        void Start()
        {
            SetActiveChildren(false);
        }

        void Update()
        {
            // Debug.Log("Update");
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (Time.timeScale == 1)
                    stopGame();
                else
                    resumeGame();
            }
        }

        private void SetActiveChildren(bool active)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(active);
            }
        }

        public void stopGame()
        {
            Time.timeScale = 0;
            SetActiveChildren(true);
            healthBarCanvas.SetActive(false);
            auraBarCanvas.SetActive(false);
        }

        public void resumeGame()
        {
            Time.timeScale = 1;
            SetActiveChildren(false);
            healthBarCanvas.SetActive(true);
            auraBarCanvas.SetActive(true);
        }

    }
}
