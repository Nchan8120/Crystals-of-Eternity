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
        currency = 100;
    }


    public void IncreaseCurrency(int amount)
    {
        currency += amount;
        Events.events["currency"]?.Invoke(currency);
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        /* HOLDS HIGHSCORE USING PLAYER PREFS
        if (PlayerPrefs.GetInt("HighScore") == 0)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        if (PlayerPrefs.GetInt("HighScore") < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
            print("HighScore set");
        }
        */
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
}
