using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour // Game ������������ ��� ��������, ������� ������ ������� � ��������� UI
{
    public Level LevelHandler;
    public UIHandler UiHandler;

    // Ememy - ������ �������� ��� ���������� ����� �����������, ������������� ��� ������� ����� � ��������.
    public GameObject Enemy;
    //���� ������ ���� ������,� ������ �������� ����������� ����� ���������, �� ��� ����� �������� �����.
    public GameObject Knigth;
    public GameObject Barbarian;

    void Start()
    {
        SetEnemy();
        LevelHandler.enemy = PickEnemy();

        UiHandler.TurnDisplay.text = "1";
        UiHandler.StageDisplay.text = "1";
        UiHandler.EnemyNameDisplay.text = LevelHandler.enemy.name;
    } 

    private Units PickEnemy()
    {

        //GameObject enemy = GameObject.Find("Enemy");
        Units unit = Enemy.GetComponentInChildren<Units>();
        return unit;
    }

    private void SetEnemy()
    {
        //GameObject.Instantiate(Knigth); // �������� � �������������� ������� Knight � �����
        //Knigth.transform.parent = Enemy.transform;  // ���������� ������� Knigth ��� �������� � �������� Enemy
        //Enemy.transform.SetParent(Knigth.transform);
        //Knigth.transform.SetParent(Enemy.transform);
        Instantiate(Barbarian, Enemy.transform);

    }
}
