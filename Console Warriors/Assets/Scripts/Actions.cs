using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

internal class Actions
{
    internal int cost = 0;

    internal LightAttack lightAttack = new LightAttack();
    internal HeavyAttack heavyAttack = new HeavyAttack();
    internal PierceAttack pierceAttack = new PierceAttack();
    internal ShieldUp shieldUp = new ShieldUp();
    internal TryToEvade tryToEvade = new TryToEvade();
    internal SkipTurn skipTurn = new SkipTurn();
    internal class LightAttack
    {
        internal int cost = 10;
        internal int damage = 25;
        internal bool DoAttack(Units attacker, Units defender)
        {
            if (!CheckEnergy(attacker, cost)) return false;
            attacker.energy -= cost;

            if (!IsTargetEvaded(defender))
            {
                float damage = Calcultate_ShieldDamage(this.damage, defender); // рассчет урона по щиту


                damage = Calculate_DamageThroughtArmor(defender, damage, "Light"); // Рассчет урона по герою с учетом доспехов
                defender.health -= damage;
                defender.CreateFloatingPoints(defender, damage, "health");

                damage = Calculate_ArmorDestruction(defender, damage, "Light"); // Рассчет урона по доспехам
                if (defender.armor != 0) defender.CreateFloatingPoints(defender, damage, "armor");
                defender.armor -= damage;

                return true;
            }
            else
            {
                defender.Evaded_Display();
                return true;
            }
        }
    }
    internal class HeavyAttack
    {
        internal int cost = 15;
        internal int damage = 20;
        internal bool DoAttack(Units attacker, Units defender)
        {
            if (!CheckEnergy(attacker, cost)) return false;
            attacker.energy -= cost;

            if (!IsTargetEvaded(defender))
            {
                float damage = Calcultate_ShieldDamage(this.damage, defender);

                damage = Calculate_DamageThroughtArmor(defender, damage, "Heavy"); // Рассчет урона по герою с учетом доспехов
                defender.health -= damage;
                defender.CreateFloatingPoints(defender, damage, "health");

                damage = Calculate_ArmorDestruction(defender, damage, "Heavy"); // Рассчет урона по доспехам
                if (defender.armor != 0) defender.CreateFloatingPoints(defender, damage, "armor");
                defender.armor -= damage;
                return true;
            }
            {
                defender.Evaded_Display();
                return true;
            }
        }
    }
    internal class PierceAttack
    {
        internal int cost = 15;
        internal int damage = 20;
        internal bool DoAttack(Units attacker, Units defender)
        {
            if (!CheckEnergy(attacker, cost)) return false;
            attacker.energy -= cost;

            if (!IsTargetEvaded(defender))
            {
                float damage = Calcultate_ShieldDamage(this.damage, defender);

                damage = Calculate_DamageThroughtArmor(defender, damage, "Pierce"); // Рассчет урона по герою с учетом доспехов
                defender.health -= damage;
                defender.CreateFloatingPoints(defender, damage, "health");

                damage = Calculate_ArmorDestruction(defender, damage, "Pierce"); // Рассчет урона по доспехам
                if (defender.armor != 0) defender.CreateFloatingPoints(defender, damage, "armor");
                defender.armor -= damage;
                return true;
            }
            {
                defender.Evaded_Display();
                return true;
            }
        }
    }
    internal class ShieldUp
    {
        internal int cost = 20;
        internal bool isUsed = false;
        internal bool Do(Units actor)
        {
            if (!CheckEnergy(actor, cost)) return false;
            actor.energy -= cost;
            actor.shield = actor.max_Shield;
            Debug.Log(actor + " Поднял щит");
            return true;
        }
    }
    internal class TryToEvade
    {
        internal int cost = 25;
        internal bool Do(Units actor)
        {
            if (!CheckEnergy(actor, cost)) return false;
            actor.energy -= cost;
            actor.effectsList.Add(new Effects.EvasionBoost(actor, 40, 4));
            return true;
        }
    }
    internal class SkipTurn
    {
        internal int cost = 0;
        internal bool Do(Units actor)
        {
            if (!CheckEnergy(actor, cost)) return false;
            actor.energy -= cost;
            return true;
        }
    }

    #region OldActions
    //public bool LightAttack(Units attacker, Units defender)
    //{
    //    if(!CheckEnergy(attacker, lightAttack_cost)) return false;
    //    attacker.energy -= lightAttack_cost;

    //    if (!IsTargetEvaded(defender))
    //    {
    //        float damage = Calcultate_ShieldDamage(attacker.LightAttack_Damage, defender); // рассчет урона по щиту


    //        damage = Calculate_DamageThroughtArmor(defender, damage, "Light"); // Рассчет урона по герою с учетом доспехов
    //        defender.health -= damage;
    //        defender.CreateFloatingPoints(defender, damage, "health");

    //        damage = Calculate_ArmorDestruction(defender, damage, "Light"); // Рассчет урона по доспехам
    //        if (defender.armor != 0) defender.CreateFloatingPoints(defender, damage, "armor");
    //        defender.armor -= damage;

    //        return true;
    //    }
    //    else
    //    {
    //        defender.Evaded_Display();
    //        return true;
    //    }
    //}
    //public bool PierceAttack(Units attacker, Units defender)
    //{
    //    if (!CheckEnergy(attacker, pierceAttack_cost)) return false;
    //    attacker.energy -= pierceAttack_cost;

    //    if (!IsTargetEvaded(defender))
    //    {
    //        float damage = Calcultate_ShieldDamage(attacker.PirceAttack_Damage, defender);

    //        damage = Calculate_DamageThroughtArmor(defender, damage, "Pierce"); // Рассчет урона по герою с учетом доспехов
    //        defender.health -= damage;
    //        defender.CreateFloatingPoints(defender, damage, "health");

    //        damage = Calculate_ArmorDestruction(defender, damage, "Pierce"); // Рассчет урона по доспехам
    //        if (defender.armor != 0) defender.CreateFloatingPoints(defender, damage, "armor");
    //        defender.armor -= damage;
    //        return true;
    //    }
    //    {
    //        defender.Evaded_Display();
    //        return true;
    //    }
    //}
    //public bool HeavyAttack(Units attacker, Units defender)
    //{
    //    if (!CheckEnergy(attacker, heavyAttack_cost)) return false;
    //    attacker.energy -= heavyAttack_cost;

    //    if (!IsTargetEvaded(defender))
    //    {
    //        float damage = Calcultate_ShieldDamage(attacker.HeavyAttack_Damage, defender);

    //        damage = Calculate_DamageThroughtArmor(defender, damage, "Heavy"); // Рассчет урона по герою с учетом доспехов
    //        defender.health -= damage;
    //        defender.CreateFloatingPoints(defender, damage, "health");

    //        damage = Calculate_ArmorDestruction(defender, damage, "Heavy"); // Рассчет урона по доспехам
    //        if (defender.armor != 0) defender.CreateFloatingPoints(defender, damage, "armor");
    //        defender.armor -= damage;
    //        return true;
    //    }
    //    {
    //        defender.Evaded_Display();
    //        return true;
    //    }
    //}
    //public bool ShieldUp(Units actor)
    //{
    //    if (!CheckEnergy(actor, shieldUp_cost)) return false;

    //    actor.energy -= shieldUp_cost;

    //    actor.shield = actor.max_Shield;

    //    Debug.Log(actor + " Поднял щит");
    //    return true;
    //}
    //public bool SkipTurn(Units actor)
    //{
    //    if (!CheckEnergy(actor, skipTurn_cost)) return false;

    //    actor.energy -= skipTurn_cost;

    //    //Debug.Log("Not implemented");
    //    return true;
    //}

    //public bool TryToEvade(Units actor)
    //{
    //    actor.effectsList.Add(new Effects.EvasionBoost(actor,40,4));
    //    return true;
    //}
    #endregion
    static float Calculate_DamageThroughtArmor(Units defender, float Damage, string attack_type) // Старый скрипт рассчета урона через броню
    {
        float pierce_Damage, origin_damage;
        pierce_Damage = 0;
        origin_damage = Damage;
        if (attack_type == "Light")
        {
            pierce_Damage = (Damage) * (1 - ((float)defender.armor / 125));
        }
        else if (attack_type == "Pierce")
        {
            pierce_Damage = (Damage) * (1 - ((float)defender.armor / 150));
        }
        else if (attack_type == "Heavy")
        {
            pierce_Damage = (Damage) * (1 - ((float)defender.armor / 100));
        }
        return pierce_Damage;
    }
    static float Calculate_ArmorDestruction(Units defender, float Damage, string attack_type) // Старый скрипт рассчета урона по броне
    {
        float Armor = 0;

        if (attack_type == "Light")
        {
            Armor = (float)((Damage + (Math.Sqrt(100) - (float)Math.Sqrt(defender.armor))) / 2);
        }
        else if (attack_type == "Pierce")
        {
            Armor = (float)((Damage + (Math.Sqrt(100) - (float)Math.Sqrt((defender.armor)))) / 8);
        }
        else if (attack_type == "Heavy")
        {
            Armor = (float)((Damage + (Math.Sqrt(100) - (float)Math.Sqrt((defender.armor)))) * 1.5);
        }

        return Armor;
    }
    static float Calcultate_ShieldDamage(float damage, Units defender) // Рассчет урона по щиту
    {
        int damage_int = Convert.ToInt32(damage);
        int damage_throgh_shield = 0;
        if (defender.shield != 0) //Если щита вовсе нет
        {
            damage_throgh_shield = damage_int - defender.shield;
            if (damage_throgh_shield < 0) // Если урон <0 значит щит не был пробит
            {
                defender.shield = defender.shield - damage_int;
                defender.CreateFloatingPoints(defender, damage_int, "shield");
                return 0f;
            }
            else
            {
                defender.shield = defender.shield - damage_int;
                defender.CreateFloatingPoints(defender, defender.shield, "shield");
                return damage_throgh_shield;
            }
        }
        else
        {
            return damage;
        }
        
    }

    static bool IsTargetEvaded(Units enemy)
    {
        int number = UnityEngine.Random.Range(0, 101);
        if (number > enemy.evasion) return false;
        else return true;
    }
    static bool CheckEnergy(Units actor, int cost)
    {
        if (actor.energy - cost > 0) return true;
        else return false;
    }

}

