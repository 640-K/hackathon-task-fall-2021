using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Entities
{
    public class Priest : Entity
    {
        public StaffOfTheSaint staff;


        public void UseStaff()
        {
            action = 1;
        }

        public void UseEffectArea()
        {
            if (!staff.effectAvailable) return;

            action = 2;
        }
    }
}
