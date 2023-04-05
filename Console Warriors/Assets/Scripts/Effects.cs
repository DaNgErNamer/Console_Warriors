using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;

/// <summary>
/// ��������� ������� ��������� �� ���������� ������ ��������, � ����������� �������� ��� ���������
/// ��������� ������� ������ ��� ����� ������ ��������� ���������
/// </summary>

[Serializable]
public class Effects
{
    [SerializeField]
    internal int value;
    [SerializeField]
    internal int turnsLeft;
    [SerializeField]
    internal Actor actor;
    /// <summary>
    /// �����������, ����� ������ ���-�� ������ ������ ���
    /// </summary>
    public virtual void DoEffect() { }
    public virtual void EndEffect() { }


    // ���� ���� ������� - ��� �������� ������������ �� �������� ������ Effects
    [Serializable]
    public class EvasionBoost : Effects
    {
        public EvasionBoost(Actor actor, int evasionValue, int turns)
        {
            this.value = evasionValue;
            this.turnsLeft = turns;
            this.actor = actor;
            Debug.Log("������ ��������� ������� �� " + turnsLeft.ToString() + " �����");
            Debug.Log("�� �������: " + actor.unit.evasion);
            actor.unit.evasion += evasionValue;
            Debug.Log("����� �������: " + actor.unit.evasion);
        }
        public override void DoEffect() 
        {
            Debug.Log("������ ��������� ��������, �������� " + turnsLeft.ToString() + " �����");
            Debug.Log("������: " + actor.unit.evasion);
        }
        public override void EndEffect()
        {
            Debug.Log("������ ��������� ������� �� " + turnsLeft.ToString() + " �����");
            actor.unit.evasion -= value;
        }
    }
}
