using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementType : MonoBehaviour
{
    public Achievement achievement;
    public TMP_Text titleUI;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (achievement != null)
        {
            GetComponent<Image>().sprite = achievement.sprite;
            titleUI.text = achievement.title;
            if (achievement.achieved)
            {
                //Debug.Log("Achieved goal!");
                GetComponent<Image>().color = Color.white;
            }
            else
            {
                GetComponent<Image>().color = Color.gray;
            }
        }
    }

    public void checkAcheivement(int amount)
    {
        if (amount > achievement.goal)
        {
            achievement.achieved = true;
            Events.events[achievement.type].RemoveListener(checkAcheivement);
            PlayerPrefs.SetInt(achievement.name, 1);
        }
    }
}
