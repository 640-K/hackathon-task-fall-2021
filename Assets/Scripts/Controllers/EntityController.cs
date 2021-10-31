using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public abstract class EntityController : MonoBehaviour
{
    public Entity controlledEntity;

    protected abstract void Update();
}
