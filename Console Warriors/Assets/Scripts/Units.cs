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
    // ���� ������������ � ������� ������������� _* - �� ������ ��������� ���-���� � ���-���� ����� ����� � ������������ �������.
    // ��� ��������� �������� ������������ �������� ��������! 
    public Units()
    {
       // Initialization();
    } // �� ��� ���� � �����, � ��� ����� ������
    public BarStatusScript UI; // UI ������ ��� ����������� ��������� ����� �� UI
    public Actions actions = new Actions();
    protected Equipment equipment = new Equipment();

    #region stats
    protected string name = "Base_Unit";
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
    protected int _max_Shield = 25;

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
    public int evasion = 0;

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
            if (_health > _max_Health) _health = _max_Health; // ������������ ������������� ����������� �������� �� ����� �������������� ���������
            UI.HealthFill = (float)((_health * 100 / _max_Health)/100);
            UI.HealthText.text = _health.ToString() + "/" + _max_Health.ToString() + " +" + _healthRest;
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
            if (_energy > _max_Energy) _energy = _max_Energy; // ������������ ������������� ����������� �������� ������� ����� �������������� ���������
            UI.EnergyFill = (float)(((float)_energy * 100 / (float)_max_Energy) / 100);
            UI.EnergyText.text = _energy.ToString() + "/" + _max_Energy.ToString() + " +" + _energyRest;
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
            if (_armor > _max_Armor) _armor = _max_Armor; // ������������ ������������� ����������� �������� ����� ����� �������������� ���������
            if (_armor < 0) _armor = 0; // ���� ��� ������������ ����� ����� ����������
            UI.ArmorFill = (float)((_armor * 100 / _max_Armor) / 100);
            UI.ArmorText.text = String.Format("{0:0.0}", _armor) + "/" + _max_Armor.ToString() + " +" + _armorRest;
        }
    }
    public int shield
    {
        get
        {
            return _shield;
        }
        set
        {
            _shield = value;
            UI.ShieldFill = (float)((_shield * 100 / _max_Shield) / 100);
            UI.ShieldText.text = _shield.ToString() + "/" + _max_Shield.ToString();
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

    public int max_Shield
    {
        get
        {
            return _max_Shield;
        }
        set
        {
            _max_Shield = value;
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
    public void Initialization()
    {
        health = health;
        shield = shield;
        armor = armor;
        energy = energy;
    }
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

