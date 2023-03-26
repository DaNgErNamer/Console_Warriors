using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public partial class Player : Units 
{
    public TMP_Text LightAttackDmg_Display;
    public TMP_Text HeavyAttackDmg_Display;
    public TMP_Text PierceAttackDmg_Display;
    public TMP_Text ShieldUpAmount_Display;

    public TMP_Text LightAttackCost_Display;
    public TMP_Text HeavyAttackCost_Display;
    public TMP_Text PierceAttackCost_Display;
    public TMP_Text ShieldUpCost_Display;
    public Player()
    {

    }
    public override void Initialization()
    {
        base.Initialization();
        LightAttackDmg_Display.text = this.LightAttack_Damage.ToString() + " DMG";
        HeavyAttackDmg_Display.text = this.HeavyAttack_Damage.ToString() + " DMG";
        PierceAttackDmg_Display.text = this.PirceAttack_Damage.ToString() + " DMG";
        ShieldUpAmount_Display.text = this.max_Shield.ToString() + " SHLD";

        LightAttackCost_Display.text = "-" + this.actions.lightAttack_cost.ToString() + " ENG";
        HeavyAttackCost_Display.text = "-" + this.actions.heavyAttack_cost.ToString() + " ENG";
        PierceAttackCost_Display.text = "-" + this.actions.pierceAttack_cost.ToString() + " ENG";
        ShieldUpCost_Display.text = "-" + this.actions.shieldUp_cost.ToString() + " ENG";
    }
    private void Start()
    {
        this.Initialization();
    }
}
