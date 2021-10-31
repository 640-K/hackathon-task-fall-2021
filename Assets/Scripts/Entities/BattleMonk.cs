using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;


namespace Entities
{
    public class BattleMonk : Entity
    {
        public KnifeOfTheSaint knife;

        public void Attack()
        {
            if (dead) return;

            action = 1;
        }

        public void Speak()
        {
            if (dead) return;

            action = 2; 
        }
    }
}