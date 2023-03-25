using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Enemy
{
    public Rogue()
    {

    }
    void Start()
    {
        max_Health = 100;
        armor = 0;
        evasion = 30;
        name = "Rogue";
        this.Initialization();
    }

}
