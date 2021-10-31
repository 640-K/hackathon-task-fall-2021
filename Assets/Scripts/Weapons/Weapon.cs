using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        public Entity owner;
        public uint damage;


        public uint damageDealt { get; protected set; }


        public abstract void Use();
    }
}
