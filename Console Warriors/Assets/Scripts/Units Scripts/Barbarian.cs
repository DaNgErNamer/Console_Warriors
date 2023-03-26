using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarian : Enemy
{ 
    public Barbarian()
    {
        unit_name = "Barbarian";

    }
    private void Start()
    {
        _health = 120;
        _max_Health = 120;
        LightAttack_Damage = 30;
    }
}
