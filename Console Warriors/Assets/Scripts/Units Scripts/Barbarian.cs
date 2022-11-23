using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Barbarian : Enemy
{
    public Barbarian() { }

    public new float health = 120;
    public new int energy = 100;
    public new float armor = 0;
    public new float LightAttack_Damage { get; set; } = 50;
}
