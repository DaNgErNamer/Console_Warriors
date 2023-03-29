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

[Serializable]
public class Units : MonoBehaviour
{
    // Поля начинающиеся с нижнего подчеркивания _* - НЕ должны изменятся где-либо и как-либо кроме этого и производного классов.
    // Для изменения значений использовать открытые свойства! 
    public Units(){} // Всё что есть у юнита, в том числе игрока

    public BarStatusScript UI; // UI скрипт для отображения состояния юнита на UI
    public Actions actions = new Actions();
    public GameObject FloatingPoints; // Префаб для отображения единиц урона в виде появляющихся цифр
    internal List<Effects> effectsList = new List<Effects>();

    #region stats
    internal string unit_name;
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
    protected int _bonuses = 3;
    protected int maxBonuses = 7;

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
            if (_health > _max_Health) _health = _max_Health; // Обеспечивает невозможность дальнейшего прироста ХП свыше установленного максимума
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
            if (_energy > _max_Energy) _energy = _max_Energy; // Обеспечивает невозможность дальнейшего прироста энергии свыше установленного максимума
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
            if (_armor > _max_Armor) _armor = _max_Armor; // Обеспечивает невозможность дальнейшего прироста брони свыше установленного максимума
            if (_armor < 0) _armor = 0; // Пока что ограничиваем броню левым диапазоном
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
            if(value<=0)
            {
                _shield = 0;
            }
            else
            {
                _shield = value;
            }
            UI.ShieldFill = (float)(((float)_shield * 100 / (float)_max_Shield) / 100);
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
    public virtual void Initialization()
    {
        health = health;
        shield = shield;
        armor = armor;
        energy = energy;
    }
    public void EffectsCheck() // Проверяем все эффекты
    {
        for (int i = effectsList.Count - 1; i >= 0; i--) // Используется обратный цикл для безпроблемного удаления эффектов, без ошибки об изменении листа.
        {
            effectsList[i].turnsLeft--;
            if (effectsList[i].turnsLeft <= 0) // Если время эффекта истекло - отменяем его действие и убираем из списка
            {
                effectsList[i].EndEffect();
                effectsList.Remove(effectsList[i]);
            }
            else // Если не истекло - эффект действует.
            {
                effectsList[i].DoEffect();
            }
        }
    }

    public void Evaded_Display()
    {
        CreateFloatingPoints(this, "Evaded", Color.white);
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
            //Destroy(this, 2f); // Освобождаем память и удаляем юнита
            //Destroy(UI, 2f);
            return true;
        }
        else return false;
    }

    public void CreateFloatingPoints (Units unit, float damage, string damageType)
    {
        GameObject points = Instantiate(FloatingPoints, transform.position, Quaternion.identity) as GameObject;
        points.transform.GetChild(0).GetComponent<TMP_Text>().text = String.Format("{0:0.0}", damage);
        switch (damageType)
        {
            case "health":
                {
                    points.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.red;
                    break;
                }
            case "armor":
                {
                    points.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.gray;
                    break;
                }
            case "shield":
                {
                    points.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.cyan;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void CreateFloatingPoints(Units unit, string text, Color color)
    {
        GameObject points = Instantiate(FloatingPoints, transform.position, Quaternion.identity) as GameObject;
        points.transform.GetChild(0).GetComponent<TMP_Text>().text = text;
        points.transform.GetChild(0).GetComponent<TMP_Text>().color = color;
    }
    #endregion

    #region AI
    public virtual void AI_Work(Units actor, Units enemy) // Базовый ИИ для юнитов, каждый потом будет перезаписывать под себя.
    {
        int random = UnityEngine.Random.Range(0, 5);
        switch (random)
        {
            case 0:
                {
                    actions.LightAttack(actor, enemy);
                    Debug.Log("Противник проводит легкую атаку");
                    break;
                }
            case 1:
                {
                    actions.HeavyAttack(actor, enemy);
                    Debug.Log("Противник проводит тяжелую атаку");
                    break;
                }
            case 2:
                {
                    actions.PierceAttack(actor, enemy);
                    Debug.Log("Противник проводит проникающую атаку");
                    break;
                }
            case 3:
                {
                    actions.ShieldUp(actor);
                    Debug.Log("Противник ставит щит");
                    break;
                }
            case 4:
                {
                    actions.SkipTurn(actor);
                    Debug.Log("Противник пропускает ход");
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
    #endregion
}
[Serializable]
public partial class Player : Units 
{ }

public partial class Enemy : Units
{ }


