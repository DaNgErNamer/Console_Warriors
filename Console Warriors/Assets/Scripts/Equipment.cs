using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    #region Equipment
    public Shields shield = new NoShield(); // ��-��������� � ����� ��� �� ����, �� ������.
    public Weapons weapon = new NoWeapon();
    #endregion
}
