using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool IsPaused = false;

    public GameObject PauseMenuUI;

    private void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameOver.GameEnd)
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;

        // Lock the cursor and make it invisible
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;

        // Unlock the cursor and make it visible
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
    }

    public void OnRestartButton()
    {
        if (LevelManager.main.gamemode == 1)
        {
            SceneManager.LoadScene(5);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

}
