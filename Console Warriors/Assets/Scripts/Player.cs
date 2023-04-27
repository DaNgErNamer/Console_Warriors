using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public partial class Player : Actor 
{
    public TMP_Text LightAttackDmg_Display;
    public TMP_Text HeavyAttackDmg_Display;
    public TMP_Text PierceAttackDmg_Display;
    public TMP_Text ShieldUpAmount_Display;
    public TMP_Text EvasionAmount_Display;

    public TMP_Text LightAttackCost_Display;
    public TMP_Text HeavyAttackCost_Display;
    public TMP_Text PierceAttackCost_Display;
    public TMP_Text ShieldUpCost_Display;
    public TMP_Text EvasionCost_Display;

    public Player() { }
    public override void Initialization()
    {
        base.Initialization();
        //LightAttackDmg_Display.text = this.unit.actions.lightAttack.damage.ToString();
        //HeavyAttackDmg_Display.text = this.unit.actions.heavyAttack.damage.ToString();
        //PierceAttackDmg_Display.text = this.unit.actions.pierceAttack.damage.ToString();
        //ShieldUpAmount_Display.text = this.unit.max_Shield.ToString();
        //EvasionAmount_Display.text = "+30"; // Шо почему просто 30, хуй знает


        //LightAttackCost_Display.text = "-" + this.unit.actions.lightAttack.cost.ToString();
        //HeavyAttackCost_Display.text = "-" + this.unit.actions.heavyAttack.cost.ToString();
        //PierceAttackCost_Display.text = "-" + this.unit.actions.pierceAttack.cost.ToString();
        //ShieldUpCost_Display.text = "-" + this.unit.actions.shieldUp.cost.ToString();
        //EvasionCost_Display.text = "-" + this.unit.actions.tryToEvade.cost.ToString();
        FillActionPanel();

    }

    private void FillActionPanel()
    {
        foreach(Actions action in this.unit.actionList)
        {
            var gameObject = Instantiate(UI.actionButton_template, UI.ActionsPanel_Grid.transform); // Создаем кнопку и получаем ссылку на неё
            Button button = FindObjectOfType<Button>(gameObject); // С помощью ссылки находим в префабе кнопку, теперь с ней можно работать
            button.GetComponentInChildren<TMP_Text>().text = action.name;
            button.onClick.AddListener(delegate { action.Clicked(gameObject); });
            this.actionButtons_List.Add(gameObject);
            // newButton.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(Number));
        }
    }

    private void Start()
    {
        this.unit = new Unit(UI, this);
        this.unit.unit_name = "player";
        this.unit.traitList.Add(Traits.traits.human);

        this.unit.actionList.Add(new Actions.LightAttack());
        this.unit.actionList.Add(new Actions.HeavyAttack());
        this.unit.actionList.Add(new Actions.PierceAttack());
        this.unit.actionList.Add(new Actions.TryToEvade());
        this.unit.actionList.Add(new Actions.SkipTurn());
        this.unit.actionList.Add(new Actions.ShieldUp());



        this.Initialization();
    }

}
