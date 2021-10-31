using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

namespace Weapons
{
    public class EffectArea : MeleeWeapon
    {
        public ParticleSystem particles;

        uint totalHealing = 0;
        uint healing = 0;

        public void ActivateArea(Entity owner, uint damage, uint healing)
        {
            this.damage = damage;
            this.healing = healing;
            this.owner = owner;

            StartCoroutine(Damage());
        }

        IEnumerator Damage()
        {
            while (isActiveAndEnabled)
            {
                Use();
                yield return new WaitForSeconds(1f);
            }
        }


        public override void Use()
        {
            uint damageDealt = 0;
            foreach (var entity in entitiesInRange)
            {
                if (entity as Vampire != null)
                    damageDealt += entity.Hurt(damage);
                else
                    totalHealing += entity.Heal(healing);
            }
        }
    }
}
