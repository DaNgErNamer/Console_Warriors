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
using TMPro;

public class Bonuses
{
    internal int value;
    internal string description;
    internal virtual void ApplyBonus(Unit actor) { }
    internal class Tier_1 : Bonuses
    {
        internal List<Bonuses> bonusList = new List<Bonuses>();
        internal Tier_1()
        {
            bonusList.Add(new Bonus_Armor_Tier1());
            bonusList.Add(new Bonus_EnergyRegen_Tier1());
            bonusList.Add(new Bonus_Evasion_Tier1());
            bonusList.Add(new Bonus_HealthRegen_Tier1());
            bonusList.Add(new Bonus_Health_Tier1());
            bonusList.Add(new Bonus_MaxHealth_Tier1());
        }
    }
    internal class Tier_2 : Bonuses
    {
        internal List<Bonuses> bonusList = new List<Bonuses>();
        internal Tier_2()
        {
            bonusList.Add(new Bonus_Health_Tier2());
            bonusList.Add(new Bonus_MaxHealth_Tier2());
        }
    }
    internal class Tier_3 : Bonuses
    {
        internal List<Bonuses> bonusList = new List<Bonuses>();
        internal Tier_3()
        {
            bonusList.Add(new Bonus_Armor_Tier3());
        }
    }

    #region Tier_1 Bonuses
    internal class Bonus_MaxHealth_Tier1 : Bonuses
    {
        internal Bonus_MaxHealth_Tier1()
        {
            value = 15;
            description = "Increasing max health by " + value.ToString();  
            
        }
        internal override void ApplyBonus(Unit actor)
        {
            actor.max_Health += value;
        }
    }

    internal class Bonus_Health_Tier1 : Bonuses
    {
        internal Bonus_Health_Tier1()
        {
            value = 45;
            description = "Increasing current health by " + value.ToString();
            
        }
        internal override void ApplyBonus(Unit actor)
        {
            actor.health += value;
        }
    }

    internal class Bonus_Armor_Tier1 : Bonuses
    {
        internal Bonus_Armor_Tier1()
        {
            value = 30;
            description = "Increasing armor by " + value.ToString();
        }
        internal override void ApplyBonus(Unit actor)
        {
            actor.armor += value;
        }
    }

    internal class Bonus_Evasion_Tier1 : Bonuses
    {
        internal Bonus_Evasion_Tier1()
        {
            value = 10;
            description = "Increasing evasion by " + value.ToString();
        }
        internal override void ApplyBonus(Unit actor)
        {
            actor.evasion += value;
        }
    }

    internal class Bonus_HealthRegen_Tier1 : Bonuses
    {
        internal Bonus_HealthRegen_Tier1()
        {
            value = 3;
            description = "Increasing health regeneration by " + value.ToString();
        }
        internal override void ApplyBonus(Unit actor)
        {
            actor.healthRest += value;
        }

    }

    internal class Bonus_EnergyRegen_Tier1 : Bonuses
    { 
        internal Bonus_EnergyRegen_Tier1()
        {
            value = 5;
            description = "Increasing energy regeneration by " + value.ToString();
        }
        internal override void ApplyBonus(Unit actor)
        {
            actor.energyRest += value;
        }
    }

    #endregion

    #region Tier_2 Bonuses
    internal class Bonus_MaxHealth_Tier2 : Bonuses
    { 
        internal Bonus_MaxHealth_Tier2()
        {
            value = 30;
            description = "Increasing max health by " + value.ToString();
        }
        internal override void ApplyBonus(Unit actor)
        {
            actor.max_Health += value;
        }
    }

    internal class Bonus_Health_Tier2 : Bonuses
    { 
        internal Bonus_Health_Tier2()
        {

            description = "Fully restores current health";

        }
        internal override void ApplyBonus(Unit actor)
        {
            value = Convert.ToInt32(actor.max_Health);
            actor.health = value;
        }
    }
    #endregion

    #region Tier_3 Bonuses
    internal class Bonus_Armor_Tier3 : Bonuses
    {
        internal Bonus_Armor_Tier3()
        {
            value = 80;
            description = "Get " + value.ToString() + " armor";
        }
        internal override void ApplyBonus(Unit actor)
        {
            actor.armor = value;
        }
    }
    #endregion
}
