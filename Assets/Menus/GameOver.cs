using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public static bool GameEnd = false;
    public GameObject GameOverUI;

    private float timerDuration = 10f; // 3 minutes in seconds
    private float timer;

    public TMP_InputField usernameInput;
    public TextMeshProUGUI winLossIndicator;
    public TextMeshProUGUI scoreIndicator;

    // Reference to the LevelManager script
    private LevelManager LM;
    private Leaderboard LB;

    private string playerScore;
    private string enemyScore;

    private bool countdownStarted = false;

    private void Start()
    {

        /*if (LevelManager.main.gamemode == 1)
        {
            timer = timerDuration;
            StartCoroutine(Countdown());
            countdownStarted = true;
        }*/

        LB = FindObjectOfType<Leaderboard>();
        if (LB == null)
        {
            Debug.LogError("Leaderboard script not found!");
        }
    }

    void Update()
    {
        if (LevelManager.main.gamemode == 0)
        {
            //Debug.Log(LevelManager.main.IsAlive());
            if (!LevelManager.main.IsAlive())
            {
                EndGameWaves();
            }
        }
        else
        {
            if (!countdownStarted) // Check if the coroutine has been started
            {
                timer = timerDuration;
                StartCoroutine(Countdown());
                countdownStarted = true;
            }
        }


        if (GameEnd)
        {
            // You can put additional logic here for the game over state if needed
        }
    }
 
    IEnumerator Countdown()
    {
        Debug.Log("timer started");
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        // The timer has reached 0, trigger game over
        EndGameTimed();
    }

    void EndGameTimed()
    {
        GameEnd = true;
        GameOverUI.SetActive(true);
        Time.timeScale = 0f;

        // end tag manager
        if (LevelManager.main != null)
        {
            // get player stats
            this.playerScore = LevelManager.main.getPlayerScore();

            scoreIndicator.text = "Score: " + this.playerScore;
        }

        winLossIndicator.text = "Time is up!";
    }

    void EndGameWaves()
    {
        GameEnd = true;
        GameOverUI.SetActive(true);
        Time.timeScale = 0f;

        // end tag manager
        if (LevelManager.main != null)
        {
            // get player stats
            this.playerScore = LevelManager.main.getPlayerScore();

            scoreIndicator.text = "Score: " + this.playerScore;
        }

        winLossIndicator.text = "You lost!";
    }

    public void OnContinue()
    {
        // get username
        string username = usernameInput.text;
        
        // save player info to leaderboard
        if (username!="")
        {
            Debug.Log(username + " scored "+ playerScore + " points!" );
            if(LB != null)
            {
                LB.Submit(username,this.playerScore);
            }
        }

        // reset scene
        GameEnd = false;
        GameOverUI.SetActive(false);
        Time.timeScale = 1f;

        // load menu scene
        SceneManager.LoadScene(0);
    }

}
