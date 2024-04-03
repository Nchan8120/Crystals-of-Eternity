using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LBBack : MonoBehaviour
{
    private void Start()
    {
        LB = FindObjectOfType<Leaderboard>();
        if (LB == null)
        {
            Debug.LogError("Leaderboard script not found!");
        }
    }

    public void OnBack()
    {
        SceneManager.LoadScene(0);
    }

    private Leaderboard LB;

    public void OnReset()
    {
        LB.ClearPrefs();
    }
}
