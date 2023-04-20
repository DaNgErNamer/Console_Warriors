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
/// Некоторые эффекты постоянны на протяжении своего действия, и применяются единожды при наложении
/// Некоторые эффекты каждый ход будут менять состояние персонажа
/// </summary>

[Serializable]
public class Effects
{
    public Effect_Mono Effect; // Объект для привязки к UI эффекту, не сериализуется

    [SerializeField]
    private int _value;
    [SerializeField]
    private int _turnsLeft;

    public virtual int value { get; set; }
    public virtual int turnsLeft { get; set; }



    [SerializeField]
    internal Actor actor;
    /// <summary>
    /// Применяется, когда эффект что-то делает каждый ход
    /// </summary>
    public virtual void DoEffect() { }
    public virtual void EndEffect() { }


    // Ниже сами эффекты - они являются производными от базового класса Effects
    [Serializable]
    public class EvasionBoost : Effects
    {
        public override int value // Позволяет управлять эффектом на UI
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
            this.Effect = effectObj; // Обязательно должно стоят первым
            this.value = evasionValue;
            this.turnsLeft = turns;
            this.actor = actor;
            Debug.Log("Эффект уклонения наложен на " + turnsLeft.ToString() + " ходов");
            Debug.Log("До эффекта: " + actor.unit.evasion);
            actor.unit.evasion += evasionValue;
            Debug.Log("После эффекта: " + actor.unit.evasion);
        }
        public override void DoEffect() 
        {
            Debug.Log("Эффект уклонения работает, осталось " + turnsLeft.ToString() + " ходов");
            Debug.Log("Эффект: " + actor.unit.evasion);
        }
        public override void EndEffect()
        {
            Debug.Log("Эффект уклонения наложен на " + turnsLeft.ToString() + " ходов");
            actor.unit.evasion -= value;
        }
    }
}
