using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    private float timerValue; // Custom timer variable
    private float sec, min;

    private void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();

        // Initialize the custom timer variable
        timerValue = 0f;
    }

    void Update()
    {
        updateTime();
    }

    private void updateTime()
    {
        // Update the custom timer variable
        timerValue += Time.deltaTime;

        min = (int)(timerValue / 60f);
        sec = (int)(timerValue % 60f);
        timer.text = min.ToString("00") + ":" + sec.ToString("00");
    }
}
