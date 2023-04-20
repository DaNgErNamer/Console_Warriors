using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;
using System;

public class Effect_Mono : MonoBehaviour
{
    public GameObject Effect;
    public TMP_Text _turnsLeft;
    public TMP_Text _value;
    public int turnsLeft
    {
        get 
        {
            return Convert.ToInt32(_turnsLeft.text);
        }
        set
        {
            if (value == 0) _turnsLeft.text = "";
            else _turnsLeft.text = value.ToString();
        }
    }

    public int value
    {
        get
        {
            return Convert.ToInt32(_value.text);
        }
        set
        {
            if (value == 0) _value.text = "";
            else _value.text = value.ToString();
        }
    }

    public void Start()
    {
        
    }

}
