using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shields
{
    public Shields() { }

    public int health = 0;
    public bool exists = false;
    public int numberOfUses = 0; // �������� �� ��������� ���������� ������������� ���� �� ���
}

public class NoShield : Shields // ���������� ����
{
    public new int health = 0;
    public new bool exists = false;
    public new int numberOfUses = 0;
}

