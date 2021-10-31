using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class VampireTeeth : MeleeWeapon
    {
        [Range(0, 1)]
        public float healingFactor = 0.35f;


        public override void Use()
        {
            if (entitiesInRange.Count == 0) return;

            Entities.Entity closest = entitiesInRange[0];
            float closestDistance = ((Vector2)(owner.transform.position - closest.transform.position)).magnitude;

            for (int i = 1; i < entitiesInRange.Count; i++)
            {
                float distance = ((Vector2)(entitiesInRange[i].transform.position - closest.transform.position)).magnitude;

                if (closestDistance > distance)
                {
                    closest = entitiesInRange[i];
                    closestDistance = distance;
                }
            }

            damageDealt = closest.Hurt(damage);
            owner.Heal((uint)(damageDealt * healingFactor));
        }
    }
}
