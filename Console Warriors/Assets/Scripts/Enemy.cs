using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Enemy : Units
{
    public Enemy() // Класс для последующего наследования для юнитов-противников.
    {

    }

    private void Start()
    {
        unit_name = "Base_Enemy";
    }
}
