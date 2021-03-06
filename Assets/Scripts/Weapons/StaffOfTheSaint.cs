using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

namespace Weapons
{
    public class StaffOfTheSaint : MeleeWeapon
    {
        public uint healing;
        public float areaEffectDuration;
        public float staffCooldown = 5f;

        public EffectArea areaPrefab;


        public override void Use()
        {
            foreach (var entity in entitiesInRange)
            {
                if (entity as Vampire != null)
                    entity.Hurt((uint)(damage * GameplayManager.instance.auraStrength));
                else
                    entity.Heal((uint)(healing * GameplayManager.instance.auraStrength));
            }
        }


        public bool effectAvailable { get; protected set; } = true;

        public virtual void ActivateArea()
        {
            if (!effectAvailable) return;

            StartCoroutine(StaffCooldown());
            StartCoroutine(StaffEffectArea());


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
                var settings = area.particles.main;
                settings.duration = areaEffectDuration;

                area.ActivateArea(owner, (uint)(damage * GameplayManager.instance.auraStrength / 2), (uint)(healing * GameplayManager.instance.auraStrength / 2));

                area.particles.Play();
                yield return new WaitForSeconds(areaEffectDuration);

                Destroy(area.gameObject);
            }
        }
    }
}
