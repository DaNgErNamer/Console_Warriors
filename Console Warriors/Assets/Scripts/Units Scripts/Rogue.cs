using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Enemy
{
    public Rogue() { }
    void Start()
    {
        unit = new Unit(UI, this);
        unit.unit_name = "Rogue";
        unit.max_Health = 100;
        unit.armor = 0;
        unit.evasion = 30;
        this.Initialization();

    }

}
