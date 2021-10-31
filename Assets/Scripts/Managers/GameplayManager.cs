using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;

public class GameplayManager : MonoBehaviour
{
    public SceneTransition lobbyTransition;
    public SceneTransition endTransition;
    public CrossOfTheDead crossPrefab;

    public static GameplayManager instance;
    

    public float dayDuration = 180f;
    
    public int currentDay = 0;


    public List<Entity> entities;
    public List<Entity> killedOnes;



    public void Start()
    {
        foreach (var entity in entities)
        {
            entity.events.onDie.AddListener((uint damage) => OnDie(entity, damage));
            entity.events.onResurrect.AddListener(() => OnResurrect(entity));
        }
        StartCoroutine(DayCoroutine());



        IEnumerator DayCoroutine()
        {
            yield return new WaitForSeconds(dayDuration);
            // start timer coroutine

            // when time is up, sum up the alive ones and caluclate aura for the next day, then deal the damage to
            // the vampire and see if he is not dead. if he is, en the game and show the results
            // if he is notset up everything for the next day and repeat the cycle, 
        }



    }
    public void OnDie(Entity entity, uint damage)
    {
        if (entity as Vampire != null) return;

        killedOnes.Add(entity);

        var obj = Instantiate(crossPrefab);
        obj.transform.position = entity.transform.position;

    }


    public void OnResurrect(Entity entity)
    {
        killedOnes.Remove(entity);
    }
}
