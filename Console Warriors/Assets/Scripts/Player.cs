using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


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
        LightAttackDmg_Display.text = this.unit.actions.lightAttack.damage.ToString() + " DMG";
        HeavyAttackDmg_Display.text = this.unit.actions.heavyAttack.damage.ToString() + " DMG";
        PierceAttackDmg_Display.text = this.unit.actions.pierceAttack.damage.ToString() + " DMG";
        ShieldUpAmount_Display.text = this.unit.max_Shield.ToString() + " SHLD";
        EvasionAmount_Display.text = "+30 EV";


        LightAttackCost_Display.text = "-" + this.unit.actions.lightAttack.cost.ToString() + " ENG";
        HeavyAttackCost_Display.text = "-" + this.unit.actions.heavyAttack.cost.ToString() + " ENG";
        PierceAttackCost_Display.text = "-" + this.unit.actions.pierceAttack.cost.ToString() + " ENG";
        ShieldUpCost_Display.text = "-" + this.unit.actions.shieldUp.cost.ToString() + " ENG";
        EvasionCost_Display.text = "-" + this.unit.actions.tryToEvade.cost.ToString() + " ENG";
    }
    private void Start()
    {
        this.unit = new Unit(UI, this);
        this.unit.unit_name = "player";
        this.unit.traitList.Add(Traits.traits.human);
        this.Initialization();
    }

}
