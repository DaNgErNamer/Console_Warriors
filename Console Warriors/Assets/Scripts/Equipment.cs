using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    #region Equipment
    public Shields shield = new NoShield(); // По-умолчанию у юнита нет ни щита, ни оружия.
    public Weapons weapon = new NoWeapon();
    #endregion
}
