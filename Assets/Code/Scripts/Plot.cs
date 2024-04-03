using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor = Color.gray;
    
    private GameObject towerObj;
    private Color startColor;
    private Turret turret;
    private TowerIce iceturret;
    private TurretSniper sniperturret;
    private TurretBomb bombturret;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHoveringUI())
        {
            return;
        }
        
        if (turret != null)
        {
            turret.OpenUpgradeUI();
            return;
        }
        if (iceturret != null)
        {
            iceturret.OpenUpgradeUI();
            return;
        }
        if (sniperturret != null)
        {
            sniperturret.OpenUpgradeUI();
            return;
        }
        if (bombturret != null)
        {
            bombturret.OpenUpgradeUI();
            return;
        }
        
        Tower towerToBuild = BuildManager.main.GetSelectedTower();
        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("Not enough money");
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);
        towerObj = Instantiate(towerToBuild.prefab, transform.position, quaternion.identity);
        LevelManager.main.tower++;
        // Invokes the event tower and passes the amount of towers placed. It is used for achievements
        Events.events["tower"]?.Invoke(LevelManager.main.tower);
        // Adds score based on the cost of tower placed. Which should be related to the tower efficiency
        LevelManager.main.IncreaseScore((int)MathF.Floor(towerToBuild.cost / 10));

        if (towerToBuild.name == "Basic Turret")
        {
            turret = towerObj.GetComponent<Turret>();
        } else if (towerToBuild.name == "Ice Turret")
        {
            iceturret = towerObj.GetComponent<TowerIce>();
        } else if (towerToBuild.name == "Sniper Turret")
        {
            sniperturret = towerObj.GetComponent<TurretSniper>();
        } else if (towerToBuild.name == "Bomb Turret")
        {
            bombturret = towerObj.GetComponent<TurretBomb>();
        }
        
    }
}
