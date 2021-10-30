using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public class BladeWeapon : Weapon
{
    protected List<Entity> entitiesInRange;

    private void Awake()
    {
        entitiesInRange = new List<Entity>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var entity = collision.GetComponent<Entity>();

        if (entity != null && entity != owner) 
            entitiesInRange.Add(entity);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var entity = collision.GetComponent<Entity>();

        if (entity != null && entity != owner)
            entitiesInRange.Remove(entity);
    }

    public override uint Use()
    {
        uint damageDealt = 0;
        foreach(var entity in entitiesInRange)
        {
            if(entity as Vampire == null)
                damageDealt += entity.Hurt(damage);
        }

        return damageDealt;
    }
}
