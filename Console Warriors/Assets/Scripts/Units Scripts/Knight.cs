using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Enemy
{
    public Knight()
    {

        unit_name = "Knight";
    }

    private void Start()
    {
        max_Health = 100;
        health = 100;
        max_Armor = 100;
        armor = 30;
        LightAttack_Damage = 20;
        healthRest = 15;
        energyRest = 8;
        this.Initialization();
    }
}
