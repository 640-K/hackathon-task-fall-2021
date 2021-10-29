using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Entities
{
    public class Vampire : Entity
    {
        
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
            action = 1;
            events.onVampireStartKnifeAttack.Invoke();
        }

        public void Bite()
        {
            action = 2;
            events.onVampireStartBite.Invoke();
        }

        public void TurnToBat()
        {
            if (state != 2)
            {
                state = 3;
                events.onVampireTurnToBat.Invoke();
            }
        }

        public void TurnToNormal()
        {
            if(state == 3)
            {
                if (motionDirection.magnitude > 0)
                    state = 1;
                else
                    state = 0;

                events.onVampireTurnToNormal.Invoke();
            }
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
