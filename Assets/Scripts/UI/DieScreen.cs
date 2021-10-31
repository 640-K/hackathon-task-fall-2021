using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Entities;

namespace UI {
    public class DieScreen : MonoBehaviour
    {
        private const string StartSceneName = "MainScene";

        public GameObject dieScreenCanvas;
        public Entity entity;

        void Start()
        {
            entity.events.onDie.AddListener(EntityDieHandler);
            SetActiveChildren(false);
        }

        void Update()
        {
            if (entity.dead)
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    // Restart the game
                    SceneManager.LoadScene(StartSceneName);
                }
            }
        }

        private void SetActiveChildren(bool active)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(active);
            }
        }

        public void EntityDieHandler()
        {
            SetActiveChildren(true);
        }

    }
}
