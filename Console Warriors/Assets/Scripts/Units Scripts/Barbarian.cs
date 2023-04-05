using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : Enemy
{
    public Barbarian() { }
    private void Start()
    {
        unit = new Unit(UI, this);
        unit.unit_name = "Barbarian";
        unit.health = 120;
        unit.max_Health = 120;
        actions.lightAttack.damage = 20;
        this.Initialization();
    }
}
