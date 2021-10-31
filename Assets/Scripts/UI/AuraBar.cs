
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Entities;

namespace UI {
    public class AuraBar : MonoBehaviour
    {
        public Image barFill;

        private float targetValue = 100f;
        public float changeRate = 0.5f;

        void Start()
        {

        }

        public void FixedUpdate()
        {
            if (targetValue == barFill.fillAmount)
                return;
            
            if (targetValue < barFill.fillAmount)
                barFill.fillAmount -= changeRate;
            else
                barFill.fillAmount += changeRate;
        }

        public void UpdateHealthBar(float value) {
            barFill.fillAmount = value;
            targetValue = value;
        }

        public void UpdateHealthBarSmoothly(float value) {
            targetValue = value;
        }
    }
}
