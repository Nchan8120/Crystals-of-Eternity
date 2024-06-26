using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private GameObject[] enemyPrefabs;
    
    [Header("Attributes")] [SerializeField]
    private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private float enemiesPerSecondCap = 15f;
   

    [Header("Events")] 
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    
    public int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesDead = 0;
    private int enemiesLeftToSpawn;
    private float eps; // enemies per second
    private bool isSpawning = false;
    public Button spawnButton;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        //StartCoroutine(StartWave());
    }

    private void Update()
    {
        if (!isSpawning)
        {
            return;
        }
        
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }
    //new   
    public void ToggleEnabled()
    {
        StartCoroutine(StartWave());
        spawnButton.interactable = false;
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
        enemiesDead++;
        Events.events["enemy"]?.Invoke(enemiesDead);
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
        isSpawning = true;
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;

        //StartCoroutine(StartWave());
        spawnButton.interactable = true;
    }
}
