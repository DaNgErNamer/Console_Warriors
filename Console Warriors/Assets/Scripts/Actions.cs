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
    private int _pierceAttack_cost = 15;
    private int _heavyAttack_cost = 15;
    private int _shieldUp_cost = 10;
    private int _skipTurn_cost = 0;
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
    #endregion

    #region actions

    public bool LightAttack(Units attacker, Units defender)
    {
        if(!CheckEnergy(attacker, lightAttack_cost)) return false;
        attacker.energy -= lightAttack_cost;

        float damage = Calculate_DamageThroughtArmor(defender, attacker.LightAttack_Damage, "Light"); // Рассчет урона по герою с учетом доспехов
        defender.health -= damage;

        damage = Calculate_ArmorDestruction(defender, attacker.LightAttack_Damage, "Light"); // Рассчет урона по доспехам
        defender.armor -= damage;
        return true;
    }
    public bool PierceAttack(Units attacker, Units defender)
    {
        if (!CheckEnergy(attacker, pierceAttack_cost)) return false;
        attacker.energy -= pierceAttack_cost;

        float damage = Calculate_DamageThroughtArmor(defender, attacker.LightAttack_Damage, "Pierce"); // Рассчет урона по герою с учетом доспехов
        defender.health -= damage;

        damage = Calculate_ArmorDestruction(defender, attacker.LightAttack_Damage, "Pierce"); // Рассчет урона по доспехам
        defender.armor -= damage;
        return true;
    }
    public bool HeavyAttack(Units attacker, Units defender)
    {
        if (!CheckEnergy(attacker, heavyAttack_cost)) return false;
        attacker.energy -= heavyAttack_cost;

        float damage = Calculate_DamageThroughtArmor(defender, attacker.LightAttack_Damage, "Heavy"); // Рассчет урона по герою с учетом доспехов
        defender.health -= damage;

        damage = Calculate_ArmorDestruction(defender, attacker.LightAttack_Damage, "Heavy"); // Рассчет урона по доспехам
        defender.armor -= damage;
        return true;
    }
    public bool ShieldUp(Units actor)
    {
        if (!CheckEnergy(actor, shieldUp_cost)) return false;

        actor.energy -= shieldUp_cost;

        actor.shield = actor.max_Shield;
        return true;
    }
    public bool SkipTurn(Units actor)
    {
        if (!CheckEnergy(actor, skipTurn_cost)) return false;

        actor.energy -= skipTurn_cost;

        //Debug.Log("Not implemented");
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
    bool CheckEnergy(Units actor, int cost)
    {
        if (actor.energy - cost > 0) return true;
        else return false;
    }
    #endregion
}
