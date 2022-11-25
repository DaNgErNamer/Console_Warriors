using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons
{
    public Weapons() { }
    public float lightAttack_Damage = 0;
    public float pierceAttack_Damage = 0;
    public float heavyAttack_Damage = 0;

}

public class NoWeapon :Weapons
{
    public new float lightAttack_Damage = 0;
    public new float pierceAttack_Damage = 0;
    public new float heavyAttack_Damage = 0;
}
