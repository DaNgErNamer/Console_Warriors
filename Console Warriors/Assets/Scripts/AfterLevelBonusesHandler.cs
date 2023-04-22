using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AfterLevelBonusesHandler : MonoBehaviour
{
    internal Bonuses bonuses;
    internal List<Bonuses> bonusList = new List<Bonuses>();
    internal int stage;
    internal Actor actor;

    internal float T1_chance;
    internal float T2_chance;
    internal float T3_chance;

    public Button action_1_btn;
    public Button action_2_btn;
    public Button action_3_btn;
    public Button action_4_btn;
    public Button action_5_btn;
    public Button action_6_btn;
    public Button action_7_btn;

    public TMP_Text btn_1_txt;
    public TMP_Text btn_2_txt;
    public TMP_Text btn_3_txt;
    public TMP_Text btn_4_txt;
    public TMP_Text btn_5_txt;
    public TMP_Text btn_6_txt;
    public TMP_Text btn_7_txt;

    public GameObject UI;

    internal void PrepareBonuses(int stage, Actor actor)
    {
        bonusList.Clear();
        this.stage = stage;
        this.actor = actor;

        UI.SetActive(true);

        T1_chance = stage * 0.5f;
        T2_chance = 100 - T3_chance + stage * 0.5f;
        T3_chance = 100 - stage * 2 - 9;

        for (int i = 0; i < 7; i++)
        {
            float chance = Random.Range(0, 100f);
            if (chance >= T1_chance)
            {
                bonusList.Add(GetBonus(1));
            }
            else if (chance >= T2_chance)
            {
                bonusList.Add(GetBonus(2));
            }
            else if (chance >= T3_chance)
            {
                bonusList.Add(GetBonus(3));
            }
        }
        SetBonuses();
    }

    private Bonuses GetBonus(int tier)
    {
        Bonuses bonus;
        switch (tier)
        {
            case 1:
                {
                    bonus = PickBonus(1);
                    return bonus;
                }
            case 2:
                {
                    bonus = PickBonus(2);
                    return bonus;
                }
            case 3:
                {
                    bonus = PickBonus(3);
                    return bonus;
                }
            default:
                {
                    return new Bonuses();
                }
        }
    }
    private Bonuses PickBonus(int tier)
    {
        Bonuses.Tier_1 T1 = new Bonuses.Tier_1();
        Bonuses.Tier_2 T2 = new Bonuses.Tier_2();
        Bonuses.Tier_3 T3 = new Bonuses.Tier_3();
        Bonuses bonus = new Bonuses();
        switch (tier)
        {
            case 1:
                {
                    int number = Random.Range(0, T1.bonusList.Count);
                    bonus = T1.bonusList[number];
                    break;
                }
            case 2:
                {
                    int number = Random.Range(0, T2.bonusList.Count);
                    bonus = T2.bonusList[number];
                    break;
                }
            case 3:
                {
                    int number = Random.Range(0, T3.bonusList.Count);
                    bonus = T3.bonusList[number];
                    break;
                }
            default:
                {
                    break;
                }
        }
        return bonus;

    }
    private void SetBonuses()
    {
        btn_1_txt.text = bonusList[0].description;
        btn_2_txt.text = bonusList[1].description;
        btn_3_txt.text = bonusList[2].description;
        btn_4_txt.text = bonusList[3].description;
        btn_5_txt.text = bonusList[4].description;
        btn_6_txt.text = bonusList[5].description;
        btn_7_txt.text = bonusList[6].description;
    }

    public void Action_1_Clicked()
    {
        bonusList[0].ApplyBonus(actor);
        UI.SetActive(false);
    }
    public void Action_2_Clicked()
    {
        bonusList[1].ApplyBonus(actor);
        UI.SetActive(false);
    }
    public void Action_3_Clicked()
    {
        bonusList[2].ApplyBonus(actor);
        UI.SetActive(false);
    }
    public void Action_4_Clicked()
    {
        bonusList[3].ApplyBonus(actor);
        UI.SetActive(false);
    }
    public void Action_5_Clicked()
    {
        bonusList[4].ApplyBonus(actor);
        UI.SetActive(false);
    }
    public void Action_6_Clicked()
    {
        bonusList[5].ApplyBonus(actor);
        UI.SetActive(false);
    }
    public void Action_7_Clicked()
    {
        bonusList[6].ApplyBonus(actor);
        UI.SetActive(false);
    }


}
