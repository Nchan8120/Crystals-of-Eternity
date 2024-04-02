using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    public static LevelManager main;
    //public Events eventManager;
    public Transform[] path;
    public Transform startPoint;

    public int currency = 1000;
    public int score = 0;
    public int tower; // tower count
    public int health = 100;

    public int gamemode; // 1 for time, 0 for health/waves

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 1000;
        score = 0;
        health = 100;
    }


    public void IncreaseCurrency(int amount)
    {
        currency += amount;
        Events.events["currency"]?.Invoke(currency);
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        Events.events["score"]?.Invoke(score);
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            //buy item
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("not enough money");
            return false;
        }
    }

    public void DecreaseHealth(int amount)
    {
        this.health -= amount;
    }

    public void IncreaseHealth(int amount)
    {
        this.health += amount;
    }

    public string getPlayerScore()
    {
        return this.score.ToString();
    }

    public bool IsAlive()
    {
        return this.health > 0;
    }
}
