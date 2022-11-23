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
    //Stats
    #region stats
    public float health { get; set; } = 100;
    public float healthRest { get; set; } = 5;
    public int energy { get; set; } = 100;
    public int energyRest { get; set; } = 10;
    public float armor { get; set; } = 0;
    public float armorRest { get; set; } = 0;
    public float evasionChance { get; set; } = 5;

    public float LightAttack_Damage { get; set; } = 10;
    public float PirceAttack_Damage { get; set; } = 10;
    public float HeavyAttack_Damage { get; set; } = 10;

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

    float Calculate_DamageThroughtArmor( Units defender,float Damage, string attack_type) // Старый скрипт рассчета урона через броню
    {
        float pierce_Damage, origin_damage, debug;
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
{

}

[Serializable]
public class Barbarian : Units
{
    public Barbarian(){}

    public new float health = 120;
    public new int energy = 100;
    public new float armor = 0;
    //public override float LightAttack(Units attacker, Units defender)
    //{
    //    defender.health -= attacker.LightAttack_Damage;
    //    return defender.health;
    //}
}
