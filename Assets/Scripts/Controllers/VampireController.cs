using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public class VampireController : EntityController
{
    Vampire controlledVampire;
    void Start()
    {
        controlledVampire = controlledEntity as Vampire;

        if (controlledVampire == null) 
            throw new ArgumentException("Controlled Entity is not a Vampire!");
    }

    void Update()
    {
        if (controlledVampire == null) return;

        if (Input.GetButtonDown("Fire1"))
            controlledVampire.KnifeAttack();

        if (Input.GetButtonDown("Fire2"))
            controlledVampire.Bite();

        ApplyMotion();
    }

    protected void ApplyMotion()
    {
        if (controlledEntity == null) return;

        Vector2 motion = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            motion += Vector2.up;
        if (Input.GetKey(KeyCode.A))
            motion -= Vector2.right;
        if (Input.GetKey(KeyCode.S))
            motion -= Vector2.up;
        if (Input.GetKey(KeyCode.D))
            motion += Vector2.right;

        controlledEntity.Move(motion);
    }

}
