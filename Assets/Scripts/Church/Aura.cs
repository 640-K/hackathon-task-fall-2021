using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public class Aura : MonoBehaviour
{
    public static Aura aura;
    

    public int villagersTotal;
    public int aliveTotal;

    public uint strength;


    public float auraLevel => aliveTotal / (float)villagersTotal;   
    public uint dailyDamageToVampires => (uint)(strength * aliveTotal);
    public Vampire vampire { get; protected set; }



    public void Awake()
    {
        if (aura == null) aura = this;
        else { Destroy(this); return; }

        DontDestroyOnLoad(this);
    }

    public void OnGameStart(Vampire vampire)
    {
        this.vampire = vampire;
    }
        
    public void OnDayEnd()
    {
        vampire.Hurt(dailyDamageToVampires);
    }

    public void OnGameEnd()
    {
        aura = null;
        Destroy(this);
    }
}
