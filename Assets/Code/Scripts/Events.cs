using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    public static Events instance;

    private UnityEvent<int> defeatEnemy = new UnityEvent<int>();
    private UnityEvent<int> addTower = new UnityEvent<int>();
    private UnityEvent<int> addCurrency = new UnityEvent<int>();
    private UnityEvent<int> addScore = new UnityEvent<int>();


    public static Dictionary<string, UnityEvent<int>> events;

    
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            events = new Dictionary<string, UnityEvent<int>>() {
                { "enemy",  defeatEnemy },
                { "tower", addTower },
                { "currency", addCurrency },
                { "score", addScore }
            };
        }
        catch(System.Exception ex)
        {
            Debug.Log("exeption occurred: " + ex.Message);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
