using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Entities
{
    public class Monk : Entity
    {
        public void Worship()
        {
            if (dead) return;
            action = 1;
        }

        public void Sacrifice()
        {
            if (dead) return;
            action = 2;
        }

    }
}
