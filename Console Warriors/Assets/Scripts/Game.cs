using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour // Game контролирует все процессы, включая логику уровней и поведение UI
{
    public Level LevelHandler;
    public UIHandler UiHandler;

    // Ememy - объект пустышка для заполнения любым противником, исключительно для единого имени и удобства.
    public GameObject Enemy;
    //Ниже список всех врагов,а именно префабов добавленных через инспектор, из них будет делаться выбор.
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
        //GameObject.Instantiate(Knigth); // Создание и иниициализация префаба Knight в сцене
        //Knigth.transform.parent = Enemy.transform;  // Назначение префаба Knigth как дочерним в пустышку Enemy
        //Enemy.transform.SetParent(Knigth.transform);
        //Knigth.transform.SetParent(Enemy.transform);
        Instantiate(Barbarian, Enemy.transform);

    }
}
