using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Barbarian : Enemy
{ 
    public Barbarian()
    {
        _health = 120;
        _max_Health = 120;
        LightAttack_Damage = 30;
    }
}
