using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : Enemy
{
    public Barbarian() { }
    private void Start()
    {
        this.unit = new Unit(UI, this);
        this.unit.unit_name = "Barbarian";
        this.unit.health = 120;
        this.unit.max_Health = 120;
        this.unit.actions.lightAttack.damage = 20;
        this.unit.armor = 5;
        this.unit.traitList.Add(Traits.traits.human);
        this.Initialization();
    }
}
