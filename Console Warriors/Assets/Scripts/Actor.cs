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

public class Actor : MonoBehaviour
{
    // Поля начинающиеся с нижнего подчеркивания _* - НЕ должны изменятся где-либо и как-либо кроме этого и производного классов.
    // Для изменения значений использовать открытые свойства! 
    public Actor() { }
    public Unit unit;  // Всё что есть у юнита, в том числе игрока
    public BarStatusScript UI; // UI скрипт для отображения состояния юнита на UI
    internal Actions actions = new Actions();
    public GameObject FloatingPoints; // Префаб для отображения единиц урона в виде появляющихся цифр

    public AudioClip s_hit;
    public Animator animator;


    #region dev_actions
    public void Start()
    {
        unit = new Unit(UI, this);
    }

    public virtual void Initialization()
    {
        unit.health = unit.health;
        unit.shield = unit.shield;
        unit.armor = unit.armor;
        unit.energy = unit.energy;
    }
    #endregion
    #region actions
    public void CreateFloatingPoints(Actor unit, float damage, string damageType)
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

    public void CreateFloatingPoints(Unit unit, string text, Color color)
    {
        GameObject points = Instantiate(FloatingPoints, transform.position, Quaternion.identity) as GameObject;
        points.transform.GetChild(0).GetComponent<TMP_Text>().text = text;
        points.transform.GetChild(0).GetComponent<TMP_Text>().color = color;
    }

    public void Serialize(Unit unit)
    {
        string fileName = "player_save.json";
        string jsonString = JsonUtility.ToJson(unit);
        File.WriteAllText(fileName, jsonString);
    }

    public Unit Deserialize()
    {
        JsonUtility.FromJsonOverwrite("player_save.json", unit);
        return unit;
    }
    #endregion

    #region AI
    public virtual void AI_Work(Actor actor, Actor enemy) // Базовый ИИ для юнитов, каждый потом будет перезаписывать под себя.
    {
        int random = UnityEngine.Random.Range(0, 5);
        switch (random)
        {
            case 0:
                {
                    actions.lightAttack.DoAttack(actor, enemy);
                    Debug.Log("Противник проводит легкую атаку");
                    break;
                }
            case 1:
                {
                    actions.heavyAttack.DoAttack(actor, enemy);
                    Debug.Log("Противник проводит тяжелую атаку");
                    break;
                }
            case 2:
                {
                    actions.pierceAttack.DoAttack(actor, enemy);
                    Debug.Log("Противник проводит проникающую атаку");
                    break;
                }
            case 3:
                {
                    actions.shieldUp.Do(actor);
                    Debug.Log("Противник ставит щит");
                    break;
                }
            case 4:
                {
                    actions.skipTurn.Do(actor);
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

public partial class Player : Actor
{ }

public partial class Enemy : Actor
{ }
