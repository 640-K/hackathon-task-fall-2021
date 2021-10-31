using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

namespace Weapons
{
    public abstract class MeleeWeapon : Weapon
    {
        public IReadOnlyList<Entity> entitiesInRange => _entitiesInRange;
        private List<Entity> _entitiesInRange;

        protected virtual void Awake()
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
    }
}
