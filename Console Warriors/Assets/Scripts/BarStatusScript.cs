using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
public class BarStatusScript : MonoBehaviour
{
    public Image Healthbar;
    public Image Enegrybar;
    public GameObject ArmorIcon;
    public GameObject ShieldIcon;

    public TMP_Text HealthText;
    public TMP_Text ShieldText;
    public TMP_Text EnergyText;
    public TMP_Text ArmorText;

    public GameObject EffectsPanel;

    public GameObject MenuWindow;
    public GameObject InventoryWindow;
    internal void Initialization(Unit actor)
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CloseOtherWindows();
            PauseGame();
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            ShowInventory();
        }
    }

    private void CloseOtherWindows()
    {
        InventoryWindow.SetActive(false);
    }

    public void PauseGame()
    {
        if (!MenuWindow.activeSelf)
        {
            Time.timeScale = 0;
            MenuWindow.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            MenuWindow.SetActive(false);
        }
    }

    public void ShowInventory()
    {
        if (!InventoryWindow.activeSelf)
        {
            Time.timeScale = 0;
            InventoryWindow.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            InventoryWindow.SetActive(false);
        }
    }
}
