using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : Actor
{
    public Enemy() { } // Класс для последующего наследования для юнитов-противников.


    private void Start()
    {
        unit = new Unit(UI, this);
        unit.unit_name = "Base_Enemy";
    }
}
