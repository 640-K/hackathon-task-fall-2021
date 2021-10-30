using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

namespace Weapons
{
    public class SplashWeapon : Weapon
    {
        public IReadOnlyList<Entity> entitiesInRange => _entitiesInRange;
        private List<Entity> _entitiesInRange;

        private void Awake()
        {
            _entitiesInRange = new List<Entity>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var entity = collision.GetComponent<Entity>();

            if (entity != null && entity != owner)
                _entitiesInRange.Add(entity);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var entity = collision.GetComponent<Entity>();

            if (entity != null && entity != owner)
                _entitiesInRange.Remove(entity);
        }

        public override void Use()
        {
            damageDealt = 0;
            foreach (var entity in _entitiesInRange)
            {
                if (entity as Vampire == null)
                    damageDealt += entity.Hurt(damage);
            }
        }
    }
}
