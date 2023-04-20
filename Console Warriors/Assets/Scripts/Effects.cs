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
    public Effect_Mono Effect; // ������ ��� �������� � UI �������, �� �������������

    [SerializeField]
    private int _value;
    [SerializeField]
    private int _turnsLeft;

    public virtual int value { get; set; }
    public virtual int turnsLeft { get; set; }



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
        public override int value // ��������� ��������� �������� �� UI
        {
            get
            {
                return _value;
            }
            set
            {
                Effect.value = value;
                _value = value;
            }
        }
        public override int turnsLeft
        {
            get
            {
                return _turnsLeft;
            }
            set
            {
                Effect.turnsLeft = value;
                _turnsLeft = value;
            }
        }
        public EvasionBoost(Actor actor, int evasionValue, int turns, Effect_Mono effectObj)
        {
            this.Effect = effectObj; // ����������� ������ ����� ������
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
