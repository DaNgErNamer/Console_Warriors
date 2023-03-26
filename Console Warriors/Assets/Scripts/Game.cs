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
    // Tier 1
    public GameObject Knigth;
    public GameObject Barbarian;
    public GameObject Rogue;




    enum Enemy_int
    {
        barbarian = 0,
        knight = 1,
        Rogue = 2 
    }


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
        int choice = Random.Range(0, 3); // ��������� ����� ����������
        switch (choice)
        {
            case 0:
                {
                    Instantiate(Barbarian, Enemy.transform);
                    break;
                }
            case 1:
                {
                    Instantiate(Knigth, Enemy.transform);
                    break;
                }
            case 2:
                {
                    Instantiate(Rogue, Enemy.transform);
                    break;
                }

            default:
                {
                    break;
                }
        }
    }
}
