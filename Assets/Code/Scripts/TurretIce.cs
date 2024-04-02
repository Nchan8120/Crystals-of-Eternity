using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TowerIce : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    
    [Header("Attribute")] 
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 4f; // Attacks per second
    [SerializeField] private float freezeTime = 1f;
    [SerializeField] private int upgradeCost = 200;
    
    private float timeUntilFire;
    private int level;
    private float baseAps;
    private float baseFreezeTime;
    private float baseTargetingRange;

    private void Start()
    {
        baseAps = aps;
        baseFreezeTime = freezeTime;
        baseTargetingRange = targetingRange;
        upgradeButton.onClick.AddListener(UpgradeTurret);
    }

    private void Update()
    {
        timeUntilFire += Time.deltaTime;
        
        if (timeUntilFire >= 1f / aps)
        {

            Freeze();
            timeUntilFire = 0f;
        }
    }

    private void Freeze()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, 
            (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            Debug.Log("Hit Enemy!");
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(0.25f);
                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }
    
    public void UpgradeTurret()
    {
        if (CalculateCost() > LevelManager.main.currency)
        {
            return;
        }

        LevelManager.main.SpendCurrency(CalculateCost());

        level++;
        aps = CalculateAps();
        targetingRange = CalculateRange();
        freezeTime = CalculateFreezeTime();
        CloseUpgradeUI();
        Debug.Log("new APS: " + aps);
        Debug.Log("new range: " + targetingRange);
        Debug.Log("new time: " + freezeTime);
        Debug.Log("new cost"+ CalculateCost());
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);
        em.ResetSpeed();
    }
    private int CalculateCost()
    {
        return Mathf.RoundToInt(upgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CalculateAps()
    {
        return (baseAps / Mathf.Pow(level, 0.3f));
    }

    private float CalculateRange()
    {
        return (baseTargetingRange * Mathf.Pow(level, 0.2f));
    }
    
    private float CalculateFreezeTime()
    {
        return (baseFreezeTime * Mathf.Pow(level, 0.2f));
    }
    
    public void OpenUpgradeUI()
    {
        upgradeUI.SetActive(true);   
    }

    public void CloseUpgradeUI()
    {
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }
    /* private void OnDrawGizmosSelected()
     {
         Handles.color = Color.cyan;
         Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
     }*/

    public void SetAPS(float attack_speed)
    {
        aps = attack_speed;
    }

    public float GetAPS()
    {
        return aps;
    }

    public void SetFreezeTime(float time)
    {
        freezeTime = time;
    }

    public float GetFreezeTime()
    {
        return freezeTime;
    }
}
