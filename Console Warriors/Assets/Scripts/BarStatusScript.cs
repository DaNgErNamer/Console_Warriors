using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BarStatusScript : MonoBehaviour
{
    public Image Healthbar;
    public Image Armorbar;
    public Image Enegrybar;
    public Image Shieldbar;

    public TMP_Text HealthText;
    public TMP_Text ShieldText;
    public TMP_Text EnergyText;
    public TMP_Text ArmorText;

    public float HealthFill;
    public float ArmorFill;
    public float EnergyFill;
    public float ShieldFill;

    // Start is called before the first frame update
    void Start()
    {
        //HealthFill = 1f;
        //ArmorFill = 1f;
        //EnergyFill = 1f;
        //ShieldFill = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Healthbar.fillAmount = HealthFill;
        Armorbar.fillAmount = ArmorFill;
        Enegrybar.fillAmount = EnergyFill;
        Shieldbar.fillAmount = ShieldFill;

    }

    internal void Initialization(Units actor)
    {
        
    }
}
