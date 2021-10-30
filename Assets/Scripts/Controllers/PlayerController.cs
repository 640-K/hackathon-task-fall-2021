using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
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

    protected override void Update() => ApplyMotion();
}