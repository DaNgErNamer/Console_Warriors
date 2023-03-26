using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public UIHandler()
    {
        StageDisplay.text = "Stage - 1";
        TurnDisplay.text = "Turn - 1";
    }


    public Button button_LightAttack;
    public Button button_PierceAttack;
    public Button button_HeavyAttack;
    public Button button_ShieldUp;
    public Button button_SkipTurn;

    public Level levelHandler;

    public bool button_LightAttack_clicked = false;
    public bool button_PierceAttack_clicked = false;
    public bool button_HeavyAttack_clicked = false;
    public bool button_ShieldUp_clicked = false;
    public bool button_SkipTurn_clicked = false;

    public TMP_Text StageDisplay;
    public TMP_Text TurnDisplay;
    public TMP_Text EnemyNameDisplay;

    public TMP_Text LightAttackDmg_Display;
    public TMP_Text HeavyAttackDmg_Display;
    public TMP_Text PierceAttackDmg_Display;
    public TMP_Text ShieldUpAmount_Display;

    public TMP_Text LightAttackCost_Display;
    public TMP_Text HeavyAttackCost_Display;
    public TMP_Text PierceAttackCost_Display;
    public TMP_Text ShieldUpCost_Display;
    

    #region buttons
    public void LightAttack_Click()
    {
        button_LightAttack_clicked = true;
        levelHandler.Level_Start(this);
    }

    public void PierceAttack_Click()
    {
        button_PierceAttack_clicked = true;
        levelHandler.Level_Start(this);
    }

    public void HeavyAttack_Click()
    {
        button_HeavyAttack_clicked = true;
        levelHandler.Level_Start(this);
    }

    public void ShieldUp()
    {
        button_ShieldUp_clicked = true;
        levelHandler.Level_Start(this);
    }

    public void SkipTurn()
    {
        button_SkipTurn_clicked = true;
        levelHandler.Level_Start(this);
    }

    public void Clear_Clicks() //—брасывает все клики
    {
        button_LightAttack_clicked = false;
        button_PierceAttack_clicked = false;
        button_HeavyAttack_clicked = false;
        button_ShieldUp_clicked = false;
        button_SkipTurn_clicked = false;
    }
    #endregion


}

