using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Entities;

namespace UI {
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        public Entity entity;

        private float targetValue = 100f;
        public float changeRate = 0.5f;

        void Start()
        {
            entity.events.onHurt.AddListener(HealthChangedListener);
            entity.events.onHeal.AddListener(HealthChangedListener);
            entity.events.onDie.AddListener(HealthChangedListener);
        }

        public void FixedUpdate()
        {
            if (targetValue == slider.value)
                return;
            
            if (targetValue < slider.value)
                slider.value -= changeRate;
            else
                slider.value += changeRate;
        }

        public void HealthChangedListener() {
            UpdateHealthBarSmoothly(entity.currentHealth);
        }

        public void UpdateHealthBar(float value) {
            slider.value = value;
            targetValue = value;
        }

        public void UpdateHealthBarSmoothly(float value) {
            targetValue = value;
        }
    }
}
