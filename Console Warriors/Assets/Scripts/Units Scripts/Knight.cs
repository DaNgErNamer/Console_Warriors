using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Enemy
{
    public Knight() { }

    private void Start()
    {
        unit = new Unit(UI, this);
        unit.unit_name = "Knight";
        unit.max_Health = 100;
        unit.health = 100;
        unit.max_Armor = 100;
        unit.armor = 30;
        actions.lightAttack.damage = 20;
        unit.healthRest = 15;
        unit.energyRest = 8;
        this.Initialization();
    }
}
