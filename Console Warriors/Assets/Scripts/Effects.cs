using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Некоторые эффекты постоянны на протяжении своего действия, и применяются единожды при наложении
/// Некоторые эффекты каждый ход будут менять состояние персонажа
/// </summary>
public class Effects
{
    internal int value;
    internal int turnsLeft;
    internal Units actor;
    /// <summary>
    /// Применяется, когда эффект что-то делает каждый ход
    /// </summary>
    public virtual void DoEffect() { }
    public virtual void EndEffect() { }


    // Ниже сами эффекты - они являются производными от базового класса Effects
    public class EvasionBoost : Effects
    {
        public EvasionBoost(Units actor, int evasionValue, int turns)
        {
            this.value = evasionValue;
            this.turnsLeft = turns;
            this.actor = actor;
            Debug.Log("Эффект уклонения наложен на " + turnsLeft.ToString() + " ходов");
            Debug.Log("До эффекта: " + actor.evasion);
            actor.evasion += evasionValue;
            Debug.Log("После эффекта: " + actor.evasion);
        }
        public override void DoEffect() 
        {
            Debug.Log("Эффект уклонения работает, осталось " + turnsLeft.ToString() + " ходов");
            Debug.Log("Эффект: " + actor.evasion);
        }
        public override void EndEffect()
        {
            Debug.Log("Эффект уклонения наложен на " + turnsLeft.ToString() + " ходов");
            actor.evasion -= value;
        }
    }
}
