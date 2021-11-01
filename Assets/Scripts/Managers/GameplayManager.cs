using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using UnityEngine.Events;
using System.Diagnostics;
using UnityEngine.UI;


public class GameplayManager : MonoBehaviour
{
    public Vampire vampire;

    public SceneTransition lobbyTransition;
    public SceneTransition loseTransition;
    public SceneTransition winTransition;
    public CrossOfTheDead crossPrefab;
    public GameObject dayPopupWindow;
    public Text dayCountLabel;
    public Text hpDealtToVampireCounter;

    public Image auraBar;
    public Text time;
    public Text score;

    public static GameplayManager instance;


    public uint auraDamage = 50;
    public float auraStrength { get; protected set; } = 0.35f;


    public float dayDuration = 180f;

    public float currentDaytime { get => Mathf.Min((float)currentDayDuration.Elapsed.TotalSeconds / dayDuration, 1f); }

    public int overallScore = 0; 
    public int currentDay = 0;
    public bool gameOver = false;




    public List<Entity> entities;
    public List<Entity> dead;


    public UnityEvent onDayBegin;
    public UnityEvent onDayEnd;
    public UnityEvent onWin;
    public UnityEvent onLose;








    Stopwatch currentDayDuration = null;
    public void Start()
    {
        if (instance != null) return;
        else instance = this;


        entities = new List<Entity>(FindObjectsOfType<Entity>());

        foreach (var entity in entities)
        {
            entity.events.onDie.AddListener((uint damage) => OnDie(entity, damage));
            entity.events.onResurrect.AddListener(() => OnResurrect(entity));
        }

        vampire.events.onDie.AddListener((uint damage) => OnLose());


        StartCoroutine(DayCoroutine());


        IEnumerator DayCoroutine()
        {
            while (!gameOver)
            {
                AtTheBeginningOfDay(); onDayBegin.Invoke();

                currentDayDuration = Stopwatch.StartNew();
                yield return new WaitForSeconds(dayDuration);
                currentDayDuration.Stop();
                AtTheEndOfDay(); onDayEnd.Invoke();
                currentDay++;
            }
        }
    }

    private void Update()
    {
        auraBar.fillAmount = auraStrength;

        var elapsed = currentDayDuration.Elapsed;
        time.text = $"Time: " + elapsed.Minutes + ":" + elapsed.Seconds;
        score.text = $"Score: {overallScore}";
    }


    public void OnDie(Entity entity, uint damage)
    {
        if (entity as Vampire != null || gameOver) return;

        dead.Add(entity);

        var obj = Instantiate(crossPrefab);
        obj.transform.position = entity.transform.position;
        obj.fallen = entity;


        if (entity as Monk != null | entity as BattleMonk != null)
            overallScore += 5;
        else if (entity as Priest != null)
            overallScore += 25;
        else
            overallScore += 3;

        int priestsCount = 0;
        foreach (var ent in entities) if (ent as Priest != null) priestsCount++;
        foreach(var d in dead) if (d as Priest != null) priestsCount--;

        if (priestsCount == 0 ||  dead.Count == entities.Count)
            OnWin();
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

        uint damageDealtToVampire = (uint)(auraDamage * auraStrength);
        vampire.Hurt(damageDealtToVampire);

        dayPopupWindow.SetActive(true);

        dayCountLabel.text = $"Day {currentDay} completed!";
        hpDealtToVampireCounter.text = $"{damageDealtToVampire} HP of damage was dealt to you by the local aura.";
    }

    public void OnWin()
    {
        if (gameOver) return;

        gameOver = true;
        onWin.Invoke();
        winTransition.Transition();
    }

    public void OnLose()
    {
        if (gameOver) return;

        gameOver = true;
        onLose.Invoke();
        loseTransition.Transition();
    }




    public void ReturnToLobby()
    {
        lobbyTransition.Transition();
    }

    public void BringUpTitlescreen()
    {
        lobbyTransition.Transition();
    }





    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }
}
