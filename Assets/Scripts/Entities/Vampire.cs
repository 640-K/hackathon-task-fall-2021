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
        public const string batModeBool = "batMode";

        public VampireTeeth teeth;
        public MeleeWeapon knife;

        public bool batMode { get => avatarAnimator.GetBool(batModeBool); set => avatarAnimator.SetBool(batModeBool, value); }


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

            batMode = true;
            speedFactor = 1.5f;
            events.onVampireTurnToBat.Invoke();
        }

        public void TurnToNormal()
        {
            if (dead) return;

            batMode = false;
            speedFactor = 1;
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
