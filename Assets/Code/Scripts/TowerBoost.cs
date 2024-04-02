using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBoost : MonoBehaviour
{
    GameObject[] Towers;
    bool isBoostingTower;
    bool isBoostingHealth;
    bool isBoostingCurrency;
    // Start is called before the first frame update
    void Start()
    {
        isBoostingTower = false;
        isBoostingHealth = false;
        isBoostingCurrency = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator C_BoostCurrency(float seconds, int amount)
    {
        isBoostingCurrency = true;
        print("Boosting currency started");

        LevelManager.main.IncreaseCurrency(amount);

        yield return new WaitForSeconds(seconds);

        isBoostingCurrency = false;
        print("Boosting currency ended");
    }

    IEnumerator C_BoostHealth(float seconds, float amount)
    {
        isBoostingHealth = true;
        print("Boosting health started");

        PlayerHealth.main.health += amount;

        yield return new WaitForSeconds(seconds);

        isBoostingHealth = false;
        print("Boosting health ended");
    }

    IEnumerator C_BoostTower(float seconds)
    {
        // Boosting starts
        isBoostingTower = true;
        print("Boosting tower started");
        Towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject tower in Towers)
        {
            // Boosts Bomb towers
            TurretBomb bomb_tower_property = tower.GetComponent<TurretBomb>();
            if (bomb_tower_property != null)
            {
                // Set the BPS of Bomb towers to +10%
                bomb_tower_property.SetBPS(bomb_tower_property.GetBPS() * 1.1f);
                // Increase damage by 1
                bomb_tower_property.SetDamage(bomb_tower_property.GetDamage() + 1);
            }

            // Boosts Ice Towers
            TowerIce ice_tower_property = tower.GetComponent<TowerIce>();
            if (ice_tower_property != null)
            {
                // Set the APS of Ice towers to +10%
                ice_tower_property.SetAPS(ice_tower_property.GetAPS() * 1.1f);
                // Increase freeze time by 10%
                ice_tower_property.SetFreezeTime(ice_tower_property.GetFreezeTime() * 1.1f);
            }

            TurretSniper sniper_tower_property = tower.GetComponent<TurretSniper>();
            if (sniper_tower_property != null)
            {
                // Set the BPS of Sniper towers to +10%
                sniper_tower_property.SetBPS(sniper_tower_property.GetBPS() * 1.1f);
                // Increase damage by 1
                sniper_tower_property.SetDamage(sniper_tower_property.GetDamage() + 1);
            }

            Turret basic_tower_property = tower.GetComponent<Turret>();
            if (basic_tower_property != null)
            {
                // Set the BPS of Basic towers to +10%
                basic_tower_property.SetBPS(basic_tower_property.GetBPS() * 1.1f);
                // Increase rotation speed by 10%
                basic_tower_property.SetRotationSpeed(basic_tower_property.GetRotationSpeed() * 1.1f);
            }
        }

            

        yield return new WaitForSeconds(seconds);


        // Boosting ends
        foreach (GameObject tower in Towers)
        {
            // Boosts Bomb towers
            TurretBomb bomb_tower_property = tower.GetComponent<TurretBomb>();
            if (bomb_tower_property != null)
            {
                // Set the BPS of Bomb towers to +10%
                bomb_tower_property.SetBPS(bomb_tower_property.GetBPS() / 1.1f);
                // Decrease damage by 1
                bomb_tower_property.SetDamage(bomb_tower_property.GetDamage() - 1);
            }

            // Boosts Ice Towers
            TowerIce ice_tower_property = tower.GetComponent<TowerIce>();
            if (ice_tower_property != null)
            {
                // Set the APS of Ice towers to +10%
                ice_tower_property.SetAPS(ice_tower_property.GetAPS() / 1.1f);
                // Decrease freeze time by 10%
                ice_tower_property.SetFreezeTime(ice_tower_property.GetFreezeTime() / 1.1f);
            }

            TurretSniper sniper_tower_property = tower.GetComponent<TurretSniper>();
            if (sniper_tower_property != null)
            {
                // Set the BPS of Sniper towers to +10%
                sniper_tower_property.SetBPS(sniper_tower_property.GetBPS() / 1.1f);
                // Decrease damage by 1
                sniper_tower_property.SetDamage(sniper_tower_property.GetDamage() - 1);
            }

            Turret basic_tower_property = tower.GetComponent<Turret>();
            if (basic_tower_property != null)
            {
                // Set the BPS of Basic towers to +10%
                basic_tower_property.SetBPS(basic_tower_property.GetBPS() / 1.1f);
                // Decrease rotation speed by 10%
                basic_tower_property.SetRotationSpeed(basic_tower_property.GetRotationSpeed() - 1.1f);
            }
        }


        isBoostingTower = false;
        print("boosting tower ended");
        
    }

    public void BoostTower(float seconds)
    {
        if (!isBoostingTower)
        {
            StartCoroutine(C_BoostTower(seconds));
        }
        
    }

    public void BoostHealth(float amount)
    {
        if (!isBoostingHealth)
        {
            StartCoroutine(C_BoostHealth(10, amount));
        }
    }

    public void BoostCurrency(int amount)
    {
        if (!isBoostingCurrency)
        {
            StartCoroutine(C_BoostCurrency(10, amount));
        }
    }

}
