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
    internal void Initialization(Unit actor)
    {
        
    }
}
