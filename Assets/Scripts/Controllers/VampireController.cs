using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public class VampireController : PlayerController
{
    Vampire controlledVampire;
    void Start()
    {
        controlledVampire = controlledEntity as Vampire;

        if (controlledVampire == null) 
            throw new ArgumentException("Controlled Entity is not a Vampire!");
    }

    protected override void Update()
    { 
        if (controlledVampire == null) return;

        if (Input.GetButtonDown("Fire1"))
            controlledVampire.KnifeAttack();

        if (Input.GetButtonDown("Fire2"))
            controlledVampire.Bite();

        if(Input.GetKeyDown(KeyCode.C))
        {
            if (controlledVampire.batMode) controlledVampire.TurnToNormal();
            else controlledVampire.TurnToBat();
        }

        base.ApplyMotion();
    }
}
