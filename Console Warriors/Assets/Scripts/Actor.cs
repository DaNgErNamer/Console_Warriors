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
    // ���� ������������ � ������� ������������� _* - �� ������ ��������� ���-���� � ���-���� ����� ����� � ������������ �������.
    // ��� ��������� �������� ������������ �������� ��������! 
    public Actor() { }
    public Unit unit;  // �� ��� ���� � �����, � ��� ����� ������
    public BarStatusScript UI; // UI ������ ��� ����������� ��������� ����� �� UI
    internal Actions actions = new Actions();
    public GameObject FloatingPoints; // ������ ��� ����������� ������ ����� � ���� ������������ ����

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
    public virtual void AI_Work(Actor actor, Actor enemy) // ������� �� ��� ������, ������ ����� ����� �������������� ��� ����.
    {
        int random = UnityEngine.Random.Range(0, 5);
        switch (random)
        {
            case 0:
                {
                    actions.lightAttack.DoAttack(actor, enemy);
                    Debug.Log("��������� �������� ������ �����");
                    break;
                }
            case 1:
                {
                    actions.heavyAttack.DoAttack(actor, enemy);
                    Debug.Log("��������� �������� ������� �����");
                    break;
                }
            case 2:
                {
                    actions.pierceAttack.DoAttack(actor, enemy);
                    Debug.Log("��������� �������� ����������� �����");
                    break;
                }
            case 3:
                {
                    actions.shieldUp.Do(actor);
                    Debug.Log("��������� ������ ���");
                    break;
                }
            case 4:
                {
                    actions.skipTurn.Do(actor);
                    Debug.Log("��������� ���������� ���");
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
