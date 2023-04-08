using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Enemy
{
    public Knight() { }

    private void Start()
    {
        this.unit = new Unit(UI, this);
        this.unit.unit_name = "Knight";
        this.unit.max_Health = 100;
        this.unit.health = 100;
        this.unit.max_Armor = 100;
        this.unit.armor = 30;
        this.actions.lightAttack.damage = 20;
        this.unit.healthRest = 15;
        this.unit.energyRest = 8;
        this.unit.traitList.Add(Traits.traits.human);
        this.Initialization();
    }
}
