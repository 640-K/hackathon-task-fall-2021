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
                    StopGame();
                else
                    ResumeGame();
            }
        }

        private void SetActiveChildren(bool active)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(active);
            }
        }

        public void StopGame()
        {
            Time.timeScale = 0;
            SetActiveChildren(true);
            healthBarCanvas.SetActive(false);
            auraBarCanvas.SetActive(false);
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            SetActiveChildren(false);
            healthBarCanvas.SetActive(true);
            auraBarCanvas.SetActive(true);
        }

    }
}
