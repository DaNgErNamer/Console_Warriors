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

public class Actions
{
    public Actions() { }
    #region private fields
    private int _lightAttack_cost = 10;
    private int _pierceAttack_cost = 20;
    private int _heavyAttack_cost = 20;
    private int _shieldUp_cost = 25;
    private int _skipTurn_cost = 0;
    private int _tryToEvade_cost = 10;
    #endregion

    #region public properties
    public int lightAttack_cost
    {
        get
        {
            return _lightAttack_cost;
        }
        set
        {
            _lightAttack_cost = value;
        }
    }
    public int pierceAttack_cost
    {
        get
        {
            return _pierceAttack_cost;
        }
        set
        {
            _pierceAttack_cost = value;
        }
    }
    public int heavyAttack_cost
    {
        get
        {
            return _heavyAttack_cost;
        }
        set
        {
            _heavyAttack_cost = value;
        }
    }
    public int shieldUp_cost
    {
        get
        {
            return _shieldUp_cost;
        }
        set
        {
            _shieldUp_cost = value;
        }
    }
    public int skipTurn_cost
    {
        get 
        {
            return _skipTurn_cost;
        }
        set
        {
            _skipTurn_cost = value;
        }
    }

    public int tryToEvade_cost { get; set; }
    #endregion

    #region actions

    public bool LightAttack(Units attacker, Units defender)
    {
        if(!CheckEnergy(attacker, lightAttack_cost)) return false;
        attacker.energy -= lightAttack_cost;

        float damage = Calcultate_ShieldDamage(attacker.LightAttack_Damage, defender); // рассчет урона по щиту


        damage = Calculate_DamageThroughtArmor(defender, damage, "Light"); // Рассчет урона по герою с учетом доспехов
        defender.health -= damage;
        defender.CreateFloatingPoints(defender, damage, "health");

        damage = Calculate_ArmorDestruction(defender, damage, "Light"); // Рассчет урона по доспехам
        if(defender.armor!=0) defender.CreateFloatingPoints(defender, damage, "armor");
        defender.armor -= damage;
        
        return true;
    }
    public bool PierceAttack(Units attacker, Units defender)
    {
        if (!CheckEnergy(attacker, pierceAttack_cost)) return false;
        attacker.energy -= pierceAttack_cost;

        float damage = Calcultate_ShieldDamage(attacker.PirceAttack_Damage, defender);

        damage = Calculate_DamageThroughtArmor(defender, damage, "Pierce"); // Рассчет урона по герою с учетом доспехов
        defender.health -= damage;
        defender.CreateFloatingPoints(defender, damage, "health");

        damage = Calculate_ArmorDestruction(defender, damage, "Pierce"); // Рассчет урона по доспехам
        if (defender.armor != 0) defender.CreateFloatingPoints(defender, damage, "armor");
        defender.armor -= damage;
        return true;
    }
    public bool HeavyAttack(Units attacker, Units defender)
    {
        if (!CheckEnergy(attacker, heavyAttack_cost)) return false;
        attacker.energy -= heavyAttack_cost;
        float damage = Calcultate_ShieldDamage(attacker.HeavyAttack_Damage, defender);

        damage = Calculate_DamageThroughtArmor(defender, damage, "Heavy"); // Рассчет урона по герою с учетом доспехов
        defender.health -= damage;
        defender.CreateFloatingPoints(defender, damage, "health");

        damage = Calculate_ArmorDestruction(defender, damage, "Heavy"); // Рассчет урона по доспехам
        if (defender.armor != 0) defender.CreateFloatingPoints(defender, damage, "armor");
        defender.armor -= damage;
        return true;
    }
    public bool ShieldUp(Units actor)
    {
        if (!CheckEnergy(actor, shieldUp_cost)) return false;

        actor.energy -= shieldUp_cost;

        actor.shield = actor.max_Shield;

        Debug.Log(actor + " Поднял щит");
        return true;
    }
    public bool SkipTurn(Units actor)
    {
        if (!CheckEnergy(actor, skipTurn_cost)) return false;

        actor.energy -= skipTurn_cost;

        //Debug.Log("Not implemented");
        return true;
    }

    public bool TryToEvade(Units actor)
    {
        actor.effectsList.Add(new Effects.EvasionBoost(actor,40,4));
        return true;
    }
    float Calculate_DamageThroughtArmor(Units defender, float Damage, string attack_type) // Старый скрипт рассчета урона через броню
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
    float Calculate_ArmorDestruction(Units defender, float Damage, string attack_type) // Старый скрипт рассчета урона по броне
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
    float  Calcultate_ShieldDamage(float damage, Units defender) // Рассчет урона по щиту
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
    bool CheckEnergy(Units actor, int cost)
    {
        if (actor.energy - cost > 0) return true;
        else return false;
    }

    #endregion
}
