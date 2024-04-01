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

    public int currency;
    public int score;
    public int tower;

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
        IncreaseScore((int)Math.Floor((float)amount/5));
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        if (PlayerPrefs.GetInt("HighScore") == 0)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        if (PlayerPrefs.GetInt("HighScore") < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
            print("HighScore set");
        }
        Events.events["currency"]?.Invoke(currency);
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
