using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class Pause : MonoBehaviour
    {
        public GameObject healthBarCanvas;
        public GameObject auraBarCanvas;
        public GameObject pause;

        void Start()
        {
            pause.SetActive(false);
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


        public void StopGame()
        {
            Time.timeScale = 0;
            pause.SetActive(true);

            healthBarCanvas.SetActive(false);
            auraBarCanvas.SetActive(false);
        }

        public void ResumeGame()
        {
            Time.timeScale = 1;
            pause.SetActive(false);
            healthBarCanvas.SetActive(true);
            auraBarCanvas.SetActive(true);
        }

    }
}
