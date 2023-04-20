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
internal class Actions
{
    [SerializeField]
    internal int cost = 0;
    [SerializeField]
    internal LightAttack lightAttack = new LightAttack();
    [SerializeField]
    internal HeavyAttack heavyAttack = new HeavyAttack();
    [SerializeField]
    internal PierceAttack pierceAttack = new PierceAttack();
    [SerializeField]
    internal ShieldUp shieldUp = new ShieldUp();
    [SerializeField]
    internal TryToEvade tryToEvade = new TryToEvade();
    [SerializeField]
    internal SkipTurn skipTurn = new SkipTurn();



    [Serializable]
    internal class BaseAction
	{
		protected void BaseAttack(Actor attacker, Actor defender)
		{
			if (attacker.animator != null)
			{
				attacker.unit.isInAnimation = true;
				attacker.animator.SetTrigger("lightAttack");
			}
		}

		protected void PlayHitSound(Actor unit)
		{
			SoundManager.instance.PlaySingle(unit.s_hit);
		}
	}
    [Serializable]
    internal class LightAttack : BaseAction
    {
        [SerializeField]
        internal int cost = 10;
        [SerializeField]
        internal int damage = 25;
        internal bool DoAttack(Actor attacker, Actor defender)
        {
            if (!CheckEnergy(attacker, cost)) return false;

			BaseAttack(attacker, defender);	//NOTE: сделано чтобы быстро сделать анимацию атаки дл€ всех типов
            attacker.unit.energy -= cost;

            if (!IsTargetEvaded(defender))
            {
                float damage = Calcultate_ShieldDamage(this.damage, defender); // рассчет урона по щиту
				PlayHitSound(defender);

                damage = Calculate_DamageThroughtArmor(defender, damage, "Light"); // –ассчет урона по герою с учетом доспехов
                defender.unit.health -= damage;
                defender.CreateFloatingPoints(defender, damage, "health");

                damage = Calculate_ArmorDestruction(defender, damage, "Light"); // –ассчет урона по доспехам
                if (defender.unit.armor != 0) defender.CreateFloatingPoints(defender, damage, "armor");
                defender.unit.armor -= damage;

                return true;
            }
            else
            {
                defender.unit.Evaded_Display();
                return true;
            }
        }
    }
    [Serializable]
    internal class HeavyAttack : BaseAction
    {
        [SerializeField]
        internal int cost = 15;
        [SerializeField]
        internal int damage = 20;
        internal bool DoAttack(Actor attacker, Actor defender)
        {
            if (!CheckEnergy(attacker, cost)) return false;

			BaseAttack(attacker, defender); //NOTE: сделано чтобы быстро сделать анимацию атаки дл€ всех типов
			attacker.unit.energy -= cost;

            if (!IsTargetEvaded(defender))
            {
                float damage = Calcultate_ShieldDamage(this.damage, defender);
				PlayHitSound(defender);

                damage = Calculate_DamageThroughtArmor(defender, damage, "Heavy"); // –ассчет урона по герою с учетом доспехов
                defender.unit.health -= damage;
                defender.CreateFloatingPoints(defender, damage, "health");

                damage = Calculate_ArmorDestruction(defender, damage, "Heavy"); // –ассчет урона по доспехам
                if (defender.unit.armor != 0) defender.CreateFloatingPoints(defender, damage, "armor");
                defender.unit.armor -= damage;
                return true;
            }
            {
                defender.unit.Evaded_Display();
                return true;
            }
        }
    }
    [Serializable]
    internal class PierceAttack : BaseAction
	{
        [SerializeField]
        internal int cost = 15;
        [SerializeField]
        internal int damage = 20;
        internal bool DoAttack(Actor attacker, Actor defender)
        {
            if (!CheckEnergy(attacker, cost)) return false;

			BaseAttack(attacker, defender); //NOTE: сделано чтобы быстро сделать анимацию атаки дл€ всех типов
			attacker.unit.energy -= cost;

            if (!IsTargetEvaded(defender))
            {
                float damage = Calcultate_ShieldDamage(this.damage, defender);
				PlayHitSound(defender);

                damage = Calculate_DamageThroughtArmor(defender, damage, "Pierce"); // –ассчет урона по герою с учетом доспехов
                defender.unit.health -= damage;
                defender.CreateFloatingPoints(defender, damage, "health");

                damage = Calculate_ArmorDestruction(defender, damage, "Pierce"); // –ассчет урона по доспехам
                if (defender.unit.armor != 0) defender.CreateFloatingPoints(defender, damage, "armor");
                defender.unit.armor -= damage;
                return true;
            }
            {
                defender.unit.Evaded_Display();
                return true;
            }
        }
    }
    [Serializable]
    internal class ShieldUp : BaseAction
	{
        [SerializeField]
        internal int cost = 20;
        [SerializeField]
        internal bool isUsed = false;
        internal bool Do(Actor actor)
        {
            if (!CheckEnergy(actor, cost)) return false;
            actor.unit.energy -= cost;
            actor.unit.shield = actor.unit.max_Shield;
            Debug.Log(actor + " ѕодн€л щит");
            return true;
        }
    }
    [Serializable]
    internal class TryToEvade : BaseAction
	{
        [SerializeField]
        internal int cost = 25;
        internal bool Do(Actor actor)
        {
            if (!CheckEnergy(actor, cost)) return false;
            actor.unit.energy -= cost;
            actor.AddEffect(new Effects.EvasionBoost(actor,40,4));
            //actor.unit.effectsList.Add(new Effects.EvasionBoost(actor, 40, 4));
            return true;
        }
    }
    [Serializable]
    internal class SkipTurn : BaseAction
	{
        [SerializeField]
        internal int cost = 0;
        internal bool Do(Actor actor)
        {
            if (!CheckEnergy(actor, cost)) return false;
            actor.unit.energy -= cost;
            return true;
        }
    }


    static float Calculate_DamageThroughtArmor(Actor defender, float Damage, string attack_type) // —тарый скрипт рассчета урона через броню
    {
        float pierce_Damage, origin_damage;
        pierce_Damage = 0;
        origin_damage = Damage;
        if (attack_type == "Light")
        {
            pierce_Damage = (Damage) * (1 - ((float)defender.unit.armor / 125));
        }
        else if (attack_type == "Pierce")
        {
            pierce_Damage = (Damage) * (1 - ((float)defender.unit.armor / 150));
        }
        else if (attack_type == "Heavy")
        {
            pierce_Damage = (Damage) * (1 - ((float)defender.unit.armor / 100));
        }
        return pierce_Damage;
    }
    static float Calculate_ArmorDestruction(Actor defender, float Damage, string attack_type) // —тарый скрипт рассчета урона по броне
    {
        float Armor = 0;

        if (attack_type == "Light")
        {
            Armor = (float)((Damage + (Math.Sqrt(100) - (float)Math.Sqrt(defender.unit.armor))) / 2);
        }
        else if (attack_type == "Pierce")
        {
            Armor = (float)((Damage + (Math.Sqrt(100) - (float)Math.Sqrt((defender.unit.armor)))) / 8);
        }
        else if (attack_type == "Heavy")
        {
            Armor = (float)((Damage + (Math.Sqrt(100) - (float)Math.Sqrt((defender.unit.armor)))) * 1.5);
        }

        return Armor;
    }
    static float Calcultate_ShieldDamage(float damage, Actor defender) // –ассчет урона по щиту
    {
        int damage_int = Convert.ToInt32(damage);
        int damage_throgh_shield = 0;
        if (defender.unit.shield != 0) //≈сли щита вовсе нет
        {
            damage_throgh_shield = damage_int - defender.unit.shield;
            if (damage_throgh_shield < 0) // ≈сли урон <0 значит щит не был пробит
            {
                defender.unit.shield = defender.unit.shield - damage_int;
                defender.CreateFloatingPoints(defender, damage_int, "shield");
                return 0f;
            }
            else
            {
                defender.unit.shield = defender.unit.shield - damage_int;
                defender.CreateFloatingPoints(defender, defender.unit.shield, "shield");
                return damage_throgh_shield;
            }
        }
        else
        {
            return damage;
        }
        
    }

    static bool IsTargetEvaded(Actor enemy)
    {
        int number = UnityEngine.Random.Range(0, 101);
        if (number > enemy.unit.evasion) return false;
        else return true;
    }
    static bool CheckEnergy(Actor actor, int cost)
    {
        if (actor.unit.energy - cost > 0) return true;
        else return false;
    }

}

