using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Entities
{
    public class Vampire : Entity
    {
        public VampireTeeth teeth;
        public BladeWeapon knife;
        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
                KnifeAttack();

            if (Input.GetButtonDown("Fire2"))
                Bite();

            ApplyMotion();
        }

        protected void ApplyMotion()
        {
            Vector2 motion = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
                motion += Vector2.up;
            if (Input.GetKey(KeyCode.A))
                motion -= Vector2.right;
            if (Input.GetKey(KeyCode.S))
                motion -= Vector2.up;
            if (Input.GetKey(KeyCode.D))
                motion += Vector2.right;

            Move(motion);
        }

        public void KnifeAttack()
        {
            if (dead) return;

            action = 1;
            events.onVampireStartKnifeAttack.Invoke();
            knife.Use();
        }

        public void Bite()
        {
            if (dead) return;

            action = 2;
            events.onVampireStartBite.Invoke();
            teeth.Use();
        }

        public void TurnToBat()
        {
            if (dead) return;

            avatarAnimator.SetBool("batMode", true);
            events.onVampireTurnToBat.Invoke();
        }

        public void TurnToNormal()
        {
            if (dead) return;

            avatarAnimator.SetBool("batMode", false);
            events.onVampireTurnToNormal.Invoke();  
        }
    }

    partial class EntityEvents
    {
        public UnityEvent onVampireStartKnifeAttack;
        public UnityEvent onVampireStartBite;
        public UnityEvent onVampireTurnToBat;
        public UnityEvent onVampireTurnToNormal;
    }
}
