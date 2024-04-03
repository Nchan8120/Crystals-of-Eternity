using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TurretBomb : MonoBehaviour
{
    [Header("References")]
    // [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    
    [Header("Attribute")] 
    [SerializeField] private float targetingRange = 4f;
    // [SerializeField] private float rotationSpeed = 200f;
    [SerializeField] private float bps = 1f; // Bullets per second
    [SerializeField] private int upgradeCost = 100;
    [SerializeField] private int damage = 2;
    

    private Transform target;
    private float timeUntilFire;
    private int level;
    private float baseBps;
    private float baseTargetingRange;

    private void Start()
    {
        baseBps = bps;
        baseTargetingRange = targetingRange;
        
        upgradeButton.onClick.AddListener(UpgradeTurret);
    }

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }
        
        //RotateTowardsTarget();
        if (!CheckTargetInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
        
    }

    /*private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }*/

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, 
            (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

/*
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) *
                      Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed* Time.deltaTime);
    }
*/

    private bool CheckTargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bomb bulletScript = bulletObj.GetComponent<Bomb>();
        bulletScript.SetTarget(target, damage);
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

    public void UpgradeTurret()
    {
        if (CalculateCost() > LevelManager.main.currency)
        {
            return;
        }

        LevelManager.main.SpendCurrency(CalculateCost());

        level++;
        bps = CalculateBps();
        targetingRange = CalculateRange();
        CloseUpgradeUI();
        Debug.Log("new BPS: " + bps);
        Debug.Log("new range: " + targetingRange);
        Debug.Log("new cost"+ CalculateCost());
    }

    private int CalculateCost()
    {
        return Mathf.RoundToInt(upgradeCost * Mathf.Pow(level, 0.8f));
    }

    private float CalculateBps()
    {
        return (baseBps * Mathf.Pow(level, 0.6f));
    }

    private float CalculateRange()
    {
        return (baseTargetingRange * Mathf.Pow(level, 0.4f));
    }
    
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }

    public void SetBPS(float bullet_speed)
    {
        bps = bullet_speed;
    }

    public float GetBPS()
    {
        return bps;
    }

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    public int GetDamage()
    {
        return damage;
    }
}
  