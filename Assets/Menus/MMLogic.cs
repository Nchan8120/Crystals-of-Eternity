using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMLogic : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1); // fix this to fit the correct scene if needed
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
}
