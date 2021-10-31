using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

namespace Weapons
{
    public class KnifeOfTheSaint : MeleeWeapon
    {
        public override void Use()
        {
            foreach (var entity in entitiesInRange)
            {
                if (entity as Vampire != null)
                {
                    damageDealt = entity.Hurt(damage);
                    break;
                }
            }
        }
    }
}
