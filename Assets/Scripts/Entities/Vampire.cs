using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Weapons;

namespace Entities
{
    public class Vampire : Entity
    {
        public VampireTeeth teeth;
        public MeleeWeapon knife;


        public void KnifeAttack()
        {
            if (dead) return;

            action = 1;
            events.onVampireStartKnifeAttack.Invoke();
        }


        public void Bite()
        {
            if (dead) return;

            action = 2;
            events.onVampireStartBite.Invoke();
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
