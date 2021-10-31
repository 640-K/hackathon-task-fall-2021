using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using UnityEngine.Events;

public class GameplayManager : MonoBehaviour
{
    public Vampire vampire;

    public SceneTransition lobbyTransition;
    public SceneTransition endTransition;
    public CrossOfTheDead crossPrefab;

    public static GameplayManager instance;


    public uint auraDamage = 50;
    public float auraStrength { get; protected set; }


    public float dayDuration = 180f;

    public int overallScore = 0; 
    public int currentDay = 0;
    public bool gameOver = false;




    public List<Entity> entities;
    public List<Entity> dead;


    public UnityEvent onDayBegin;
    public UnityEvent onDayEnd;
    public UnityEvent onWin;
    public UnityEvent onLose;


    public void Start()
    {
        if (instance != null) return;
        else instance = this;

        foreach (var entity in entities)
        {
            entity.events.onDie.AddListener((uint damage) => OnDie(entity, damage));
            entity.events.onResurrect.AddListener(() => OnResurrect(entity));
        }


        StartCoroutine(DayCoroutine());


        IEnumerator DayCoroutine()
        {
            while (!gameOver)
            {
                AtTheBeginningOfDay(); onDayBegin.Invoke();
                yield return new WaitForSeconds(dayDuration);
                AtTheEndOfDay(); onDayEnd.Invoke();
                currentDay++;
            }
        }
    }




    public void OnDie(Entity entity, uint damage)
    {
        if (entity as Vampire != null || gameOver) return;

        dead.Add(entity);

        var obj = Instantiate(crossPrefab);
        obj.transform.position = entity.transform.position;


        if (entity as Monk != null | entity as BattleMonk != null)
            overallScore += 5;
        else if (entity as Priest != null)
            overallScore += 25;
        else
            overallScore += 3;

        if (entity as Priest != null ||  dead.Count == entities.Count)
        {
            gameOver = true;
            OnWin();
            onWin.Invoke();
        }
    }


    public void OnResurrect(Entity entity)
    {
        if (entity as Vampire != null || gameOver) return;

        entity.believerLevel += 0.5f;
        dead.Remove(entity);
    }


    
    
    
    public void AtTheBeginningOfDay()
    {

    }


    public void AtTheEndOfDay()
    {
        float believerLevel = 0f;
        foreach(var entity in entities)
            if(!dead.Contains(entity))
                believerLevel += entity.believerLevel;

       auraStrength = believerLevel / entities.Count;

        vampire.Hurt((uint)(auraDamage * auraStrength));

        if(vampire.dead)
        {
            gameOver = true;
            OnLose();
            onLose.Invoke();
        }
    }

    public void OnWin()
    {

    }

    public void OnLose()
    {

    }




    public void ReturnToLobby()
    {
        lobbyTransition.Transition();
    }

    public void BringUpTitlescreen()
    {
        endTransition.Transition();
    }





    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }
}
