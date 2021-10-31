using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public string exitToScene;
    public static GameplayManager instance;

    private void Start()
    {
        
    }

    public void Stop()
    {
        Destroy(this);


        
    }
}
