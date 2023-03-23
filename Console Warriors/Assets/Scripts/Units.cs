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
    // Поля начинающиеся с нижнего подчеркивания _* - НЕ должны изменятся где-либо и как-либо кроме этого и производного классов.
    // Для изменения значений использовать открытые свойства! 
    public Units() { } // Всё что есть у юнита, в том числе игрока
    public BarStatusScript UI; // UI скрипт для отображения состояния юнита на UI
    public Actions actions = new Actions();

    #region stats
    protected float _health=100;
    protected float _healthRest = 5;
    protected int _energy = 100;
    protected int _energyRest = 10;
    protected float _armor = 0;
    protected float _armorRest = 0;
    protected float _max_Health = 100;
    protected int _max_Energy = 100;
    protected float _max_Armor = 100;
    protected int _shield = 0;

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
            if (_health > _max_Health) _health = _max_Health; // Обеспечивает невозможность дальнейшего прироста ХП свыше установленного максимума
            UI.HealthFill = (float)((_health * 100 / _max_Health)/100);
            UI.HealthText.text = _health.ToString() + "/" + _max_Health.ToString() + "+" + _healthRest;
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
            if (_energy > _max_Energy) _energy = _max_Energy; // Обеспечивает невозможность дальнейшего прироста энергии свыше установленного максимума
            UI.EnergyFill = (float)(((float)_energy * 100 / (float)_max_Energy) / 100);
            UI.EnergyText.text = _energy.ToString() + "/" + _max_Energy.ToString() + "+" + _energyRest;
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
            if (_armor > _max_Armor) _armor = _max_Armor; // Обеспечивает невозможность дальнейшего прироста брони свыше установленного максимума
            if (_armor < 0) _armor = 0; // Пока что ограничиваем броню левым диапазоном
            UI.ArmorFill = (float)((_armor * 100 / _max_Armor) / 100);
            UI.ArmorText.text = _armor.ToString() + "/" + _max_Armor.ToString() + "+" + _armorRest;
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
    public int max_Energy
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

    #region actions
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

    #region devActions
    void Start()
    {

    }
    public bool IsDead()
    {
        if (this.health <= 0) return true;
        else return false;
    }
    #endregion
}
[Serializable]
public partial class Player : Units 
{ }

public partial class Enemy : Units
{ }

