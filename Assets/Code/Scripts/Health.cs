using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")] 
    [SerializeField] private int hitPoints = 2;

    [SerializeField] private int currencyWorth = 50;

    private bool isDestroyed = false;
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            // Increases score depending on the worth of the enemy
            LevelManager.main.IncreaseScore((int)Mathf.Floor(currencyWorth / 5));
            Destroy(gameObject);
        }
    }
}
