using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Long_Sword : Weapon_Mono
{
    public Long_Sword() { }
    public void ApplyBonus(Unit unit)
    {
        unit.actions.lightAttack.damage += 5;
        unit.actions.heavyAttack.damage += 1;
        unit.actions.pierceAttack.damage += 3;
    }
}
