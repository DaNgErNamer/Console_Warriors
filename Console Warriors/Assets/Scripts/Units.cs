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

[Serializable]
public class Units : MonoBehaviour
{
    public Units() { } // Всё что есть у юнита, в том числе игрока
    public BarStatusScript UI;
    //Stats
    #region stats
    protected float _health=100;
    protected float _healthRest = 5;
    protected int _energy = 100;
    protected int _energyRest = 10;
    protected float _armor = 0;
    protected float _armorRest = 0;
    protected float _max_Health = 100;
    protected float _max_Energy = 100;
    protected float _max_Armor = 100;

    public float max_Armor
    {
        get
        {
            return _max_Armor;
        }

        set
        {
            _max_Armor = value;
        }
    }
    public float evasionChance { get; set; } = 5;

    public float LightAttack_Damage { get; set; } = 10;
    public float PirceAttack_Damage { get; set; } = 10;
    public float HeavyAttack_Damage { get; set; } = 10;

    #endregion
    #region stats-properties
    public float health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            UI.HealthFill = (float)((_health * 100 / max_Health)/100);
        }
    }
    public float healthRest
    {
        get
        {
            return _healthRest;
        }
        set
        {
            _healthRest = value;
        }
    }
    public int energy
    {
        get
        {
            return _energy;
        }
        set
        {
            _energy = value;
        }
    }
    public int energyRest
    {
        get
        {
            return _energyRest;
        }
        set
        {
            _energyRest = value;
        }
    }
    public float armor
    {
        get
        {
            return _armor;
        }
        set
        {
            _armor = value;
            if (_armor < 0) _armor = 0;
        }
    }
    public float armorRest
    {
        get
        {
            return _armorRest;
        }
        set
        {
            _armorRest = value;
        }
    }
    public float max_Health
    {
        get
        {
            return _max_Health;
        }
        set
        {
            _max_Health = value;
        }
    }
    public float max_Energy
    {
        get
        {
            return _max_Energy;
        }
        set
        {
            _max_Energy = value;
        }
    }
    #endregion
    // Actions
    #region actions
    public void LightAttack(Units attacker, Units defender)
    {
        float damage = Calculate_DamageThroughtArmor(defender,attacker.LightAttack_Damage, "Light"); // Рассчет урона по герою с учетом доспехов
        defender.health -= damage; 

        damage = Calculate_ArmorDestruction(defender, attacker.LightAttack_Damage, "Light"); // Рассчет урона по доспехам
        defender.armor -= damage;
    }
    public void PierceAttack(Units attacker, Units defender)
    {
        float damage = Calculate_DamageThroughtArmor(defender, attacker.LightAttack_Damage, "Pierce"); // Рассчет урона по герою с учетом доспехов
        defender.health -= damage;

        damage = Calculate_ArmorDestruction(defender, attacker.LightAttack_Damage, "Pierce"); // Рассчет урона по доспехам
        defender.armor -= damage;
    }
    public void HeavyAttack(Units attacker, Units defender)
    {
        float damage = Calculate_DamageThroughtArmor(defender, attacker.LightAttack_Damage, "Heavy"); // Рассчет урона по герою с учетом доспехов
        defender.health -= damage;

        damage = Calculate_ArmorDestruction(defender, attacker.LightAttack_Damage, "Heavy"); // Рассчет урона по доспехам
        defender.armor -= damage;
    }

    public void ShieldUp(Units actor)
    {
        Debug.Log("Not implemented");
    }

    public void SkipTurn(Units actor)
    {
        Debug.Log("Not implemented");
    }

    float Calculate_DamageThroughtArmor( Units defender,float Damage, string attack_type) // Старый скрипт рассчета урона через броню
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
       float Armor=0;

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
    public void Rest()
    {
        this.health += healthRest;
        this.energy += energyRest;
        this.armor += armorRest;
    }
    #endregion

    #region Equipment
    public Shields shield = new NoShield(); // По-умолчанию у юнита нет ни щита, ни оружия.
    public Weapons weapon = new NoWeapon();
    #endregion
    // Start is called before the first frame update
    void Start()
    {

    }
}
[Serializable]
public partial class Player : Units 
{ }

public partial class Enemy : Units
{ }

[Serializable]
public partial class Barbarian : Enemy
{

}
