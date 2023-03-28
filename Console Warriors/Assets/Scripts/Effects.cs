using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ������� ��������� �� ���������� ������ ��������, � ����������� �������� ��� ���������
/// ��������� ������� ������ ��� ����� ������ ��������� ���������
/// </summary>
public class Effects
{
    internal int value;
    internal int turnsLeft;
    internal Units actor;
    /// <summary>
    /// �����������, ����� ������ ���-�� ������ ������ ���
    /// </summary>
    public virtual void DoEffect() { }
    public virtual void EndEffect() { }


    // ���� ���� ������� - ��� �������� ������������ �� �������� ������ Effects
    public class EvasionBoost : Effects
    {
        public EvasionBoost(Units actor, int evasionValue, int turns)
        {
            this.value = evasionValue;
            this.turnsLeft = turns;
            this.actor = actor;
            Debug.Log("������ ��������� ������� �� " + turnsLeft.ToString() + " �����");
            Debug.Log("�� �������: " + actor.evasion);
            actor.evasion += evasionValue;
            Debug.Log("����� �������: " + actor.evasion);
        }
        public override void DoEffect() 
        {
            Debug.Log("������ ��������� ��������, �������� " + turnsLeft.ToString() + " �����");
            Debug.Log("������: " + actor.evasion);
        }
        public override void EndEffect()
        {
            Debug.Log("������ ��������� ������� �� " + turnsLeft.ToString() + " �����");
            actor.evasion -= value;
        }
    }
}
