using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public class Aura : MonoBehaviour
{
    public static Aura aura;
    

    public uint villagersTotal;
    public uint aliveTotal;

    public uint antiVampireDamage;


    public float strength => aliveTotal / (float)villagersTotal;   
    public uint dailyDamageToVampires => (uint)(antiVampireDamage * aliveTotal);
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
