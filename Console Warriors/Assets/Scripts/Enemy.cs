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
        name = "Base_Enemy";
    }
}
