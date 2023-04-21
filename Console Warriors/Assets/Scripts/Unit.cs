using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
//using Newtonsoft.Json;
using UnityEngine;
using TMPro;

[Serializable]
public class Unit
{
    [NonSerialized]
    protected BarStatusScript UI;
    [NonSerialized]
    protected Actor actor;
    public Unit(BarStatusScript UI, Actor actor)
    {
        this.UI = UI;
        this.actor = actor;
    }
    [SerializeField]
    internal Actions actions = new Actions();

    #region stats
    [SerializeField]
    public List<Traits.traits> traitList = new List<Traits.traits>();
    [SerializeField]
    internal List<Effects> effectsList = new List<Effects>();
    [SerializeField]
    internal string unit_name;
    [SerializeField]
    protected float _health = 100;
    [SerializeField]
    protected float _healthRest = 5;
    [SerializeField]
    protected int _energy = 100;
    [SerializeField]
    protected int _energyRest = 10;
    [SerializeField]
    protected float _armor = 0;
    [SerializeField]
    protected float _armorRest = 0;
    [SerializeField]
    protected float _max_Health = 100;
    [SerializeField]
    protected int _max_Energy = 100;
    [SerializeField]
    protected float _max_Armor = 100;
    [SerializeField]
    protected int _shield = 0;
    [SerializeField]
    protected int _max_Shield = 25;
    [SerializeField]
    protected int _bonuses = 3;
    [SerializeField]
    protected int maxBonuses = 7;
    [SerializeField]
    public volatile bool isInAnimation = false;		//NOTE: важное замечание для сохранений - нужно хранить тип анимации и "кадр"!
    public int evasion = 0;



    #endregion

    #region stats-properties
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
            UI.Healthbar.fillAmount = (float)((_health * 100 / _max_Health) / 100);
            UI.HealthText.text = String.Format("{0:0.0}", _health) + "/" + _max_Health.ToString() + " +" + _healthRest;
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
            UI.Enegrybar.fillAmount = (float)(((float)_energy * 100 / (float)_max_Energy) / 100);
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
            if (_armor > _max_Armor) _armor = _max_Armor; // Обеспечивает невозможность дальнейшего прироста брони свыше установленного максимума
            if (_armor < 0) _armor = 0; // Пока что ограничиваем броню левым диапазоном

            if (_armor > 0) UI.ArmorIcon.SetActive(true); // Включает/отключает отображение иконки
            else UI.ArmorIcon.SetActive(false);

            if (_armorRest > 0) // Если броня восстанавливается
                UI.ArmorText.text = String.Format("{0:0}", _armor) + " +" + _armorRest;
            else // Если броня не восстанавливается
                UI.ArmorText.text = String.Format("{0:0}", _armor);
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
            if (value <= 0)
            {
                _shield = 0;
            }
            else
            {
                _shield = value;
            }

            if (_shield > 0) UI.ShieldIcon.SetActive(true); // Включает/отключает отображение иконки
            else UI.ShieldIcon.SetActive(false);
            UI.ShieldText.text = _shield.ToString();
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

    public int bonuses
    {
        get
        {
            return _bonuses;
        }
        set
        {
            _bonuses = value;
            if (_bonuses > maxBonuses) _bonuses = maxBonuses;
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

    public void EffectsCheck() // Проверяем все эффекты
    {
        for (int i = effectsList.Count - 1; i >= 0; i--) // Используется обратный цикл для безпроблемного удаления эффектов, без ошибки об изменении листа.
        {
            effectsList[i].turnsLeft--;
            if (effectsList[i].turnsLeft <= 0) // Если время эффекта истекло - отменяем его действие и убираем из списка
            {
                effectsList[i].EndEffect();
                RemoveEffect(i);
            }
            else // Если не истекло - эффект действует.
            {
                effectsList[i].DoEffect();
            }
        }
    }

    private void RemoveEffect(int i)
    {
        Effects effect = effectsList[i];
        effectsList.Remove(effectsList[i]);
        actor.RemoveEffect(effect);
    }

    internal void AddEffectToUnit(Effects effect)
    {
        effectsList.Add(effect);
    }

    public void Evaded_Display()
    {
        actor.CreateFloatingPoints(this, "Evaded", Color.white);
    }

    public void AnimationEnded()
    {
        isInAnimation = false;
    }
    #endregion

    #region devActions
    void Start()
    {
        //this.UI.Initialization(this);
    }
    public bool IsDead()
    {
        if (this.health <= 0)
        {
            return true;
        }
        else return false;
    }


    #endregion
}



