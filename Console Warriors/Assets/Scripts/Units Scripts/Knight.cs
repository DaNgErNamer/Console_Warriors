using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Enemy
{
    public Knight()
    {

    }

    private void Start()
    {
        max_Health = 150;
        health = 150;
        max_Armor = 100;
        armor = 50;
        LightAttack_Damage = 20;
        healthRest = 15;
        energyRest = 30;
        unit_name = "Knight";
        this.Initialization();
    }
}
