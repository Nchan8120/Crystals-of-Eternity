using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private TextMeshProUGUI currencyUI;
    [SerializeField] private TextMeshProUGUI scoreUI;

    [SerializeField] private Animator anim;

    private bool isMenuOpen = true;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
        scoreUI.text = LevelManager.main.score.ToString();
    }
}
