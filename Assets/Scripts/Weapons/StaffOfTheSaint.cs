using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

namespace Weapons
{
    public class StaffOfTheSaint : SplashWeapon
    {
        public float areaEffectDuration;
        public float staffCooldown = 5f;

        public EffectArea areaPrefab;


        public override void Use()
        {
            foreach (var entity in entitiesInRange)
            {
                if (entity as Vampire != null)
                    entity.Hurt((uint)(damage * Aura.aura.auraLevel));
                else
                    entity.Heal((uint)(damage * Aura.aura.auraLevel));
            }
        }


        bool effectAvailable = true;

        public virtual bool ActivateArea()
        {
            if (!effectAvailable) return false;

            StartCoroutine(StaffCooldown());
            StartCoroutine(StaffEffectArea());

            return true;


            IEnumerator StaffCooldown()
            {
                effectAvailable = false;
                yield return new WaitForSeconds(staffCooldown);
                effectAvailable = true;
            }

            IEnumerator StaffEffectArea()
            {
                var area = Instantiate(areaPrefab);
                area.transform.position = transform.position;

                yield return new WaitForSeconds(areaEffectDuration);

                Destroy(area);
            }
        }
    }
}
