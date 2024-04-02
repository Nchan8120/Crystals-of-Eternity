using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class PopulateAchievements : MonoBehaviour
{
    public GameObject achievementPrefab;
    public Achievement[] achievementTypes;
    //public Events eventManager;
    // Start is called before the first frame update
    void Start()
    {
        achievementTypes = Resources.LoadAll<Achievement>("Scriptable Objects/Achievement");
        for (int i = 0; i < achievementTypes.Length; i++)
        {
            GameObject newAchievement = Instantiate(achievementPrefab);
            newAchievement.transform.SetParent(transform);
            AchievementType achievementType = newAchievement.GetComponent<AchievementType>();
            achievementType.achievement = achievementTypes[i];

            //eventManager = GameObject.FindGameObjectWithTag("Event System").GetComponent<Events>();

            // adds a listener in the event manager for the appropriate achievement type
            //eventManager.events[achievementType.achievement.type].AddListener(achievementType.checkAcheivement);
            if (PlayerPrefs.GetInt(achievementType.achievement.name) == 0)
            {
                Events.events[achievementType.achievement.type].AddListener(achievementType.checkAcheivement);
            }
            else
            {
                achievementType.achievement.achieved = true;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
