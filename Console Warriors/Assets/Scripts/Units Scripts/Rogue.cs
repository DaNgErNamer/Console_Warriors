using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Enemy
{
    public Rogue() { }
    void Start()
    {
        this.unit = new Unit(UI, this);
        this.unit.unit_name = "Rogue";
        this.unit.max_Health = 100;
        this.unit.armor = 0;
        this.unit.evasion = 30;
        this.unit.traitList.Add(Traits.traits.human);
        this.Initialization();
    }
}
