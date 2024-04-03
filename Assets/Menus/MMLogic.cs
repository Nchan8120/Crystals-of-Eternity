using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMLogic : MonoBehaviour
{

    public GameObject playButton;
    public GameObject timedButton;
    public GameObject wavesButton;

    public GameObject achievementsPanel;


    public void OnPlayButton()
    {
        playButton.SetActive(false);
        timedButton.SetActive(true);
        wavesButton.SetActive(true);
    }

    public void OnTimedButton()
    {
        SceneManager.LoadScene("HealthBar"); // fix this to fit the correct scene if needed
    }

    public void OnWavesButton()
    {
        SceneManager.LoadScene("WaveModeZen"); // fix this to fit the correct scene if needed
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnLeaderboardButton()
    {
        SceneManager.LoadScene(2); 
    }

    public void OnSettingsButton()
    {
        SceneManager.LoadScene(4);
    }

    public void OnUpdgradeBayButton()
    {
        SceneManager.LoadScene(3);
    }

    public void OnAchievementsButton()
    {
        achievementsPanel.SetActive(true);
    }
}
