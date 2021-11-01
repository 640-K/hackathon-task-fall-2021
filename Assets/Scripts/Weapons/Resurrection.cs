using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Resurrection : Weapon
    {
        public float resurrectChance = 25f;
        public IReadOnlyList<CrossOfTheDead> crossesesInRange => _crossesInRange;
        private List<CrossOfTheDead> _crossesInRange;
        public ParticleSystem particles;

        protected virtual void Awake()
        {
            _crossesInRange = new List<CrossOfTheDead>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var entity = collision.GetComponent<CrossOfTheDead>();

            if (entity != null && entity != owner)
                _crossesInRange.Add(entity);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var entity = collision.GetComponent<CrossOfTheDead>();

            if (entity != null && entity != owner)
                _crossesInRange.Remove(entity);
        }

        public override void Use()
        {
            foreach (var cross in crossesesInRange)
            {
                if(Random.Range(0f, 100f) < resurrectChance)
                {
                    cross.fallen.Resurrect();
                    Destroy(cross.gameObject);
                    particles.Play();

                    return;
                }
            }
        }
    }
}
