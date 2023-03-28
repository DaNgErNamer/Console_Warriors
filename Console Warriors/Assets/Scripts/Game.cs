using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour // Game контролирует все процессы, включа€ логику уровней и поведение UI
{
    public Level LevelHandler;
    public UIHandler UiHandler;

    // Ememy - объект пустышка дл€ заполнени€ любым противником, исключительно дл€ единого имени и удобства.
    public GameObject Enemy;

    //Ќиже список всех врагов,а именно префабов добавленных через инспектор, из них будет делатьс€ выбор.
    // Tier 1
    public GameObject Knigth;
    public GameObject Barbarian;
    public GameObject Rogue;
    protected GameObject InstantiatedUnit;


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
        UiHandler.EnemyNameDisplay.text = LevelHandler.enemy.unit_name;
        UiHandler.turn = 1;
    } 

    private Units PickEnemy()
    {
        Units unit = Enemy.GetComponentInChildren<Units>();
        return unit;
    }

    private void SetEnemy()
    {
        int choice = Random.Range(0, 3); // –андомный выбор персонажей
        switch (choice)
        {
            case 0:
                {
                    InstantiatedUnit = Instantiate(Barbarian, Enemy.transform);
                    break;
                }
            case 1:
                {
                    InstantiatedUnit = Instantiate(Knigth, Enemy.transform);
                    break;
                }
            case 2:
                {
                    InstantiatedUnit = Instantiate(Rogue, Enemy.transform);
                    break;
                }

            default:
                {
                    break;
                }
        }
    }

    public void NextStage()
    {
        StartCoroutine(NextStageCoroutine());
    }
    private IEnumerator NextStageCoroutine()
    {
        Destroy(InstantiatedUnit);
        yield return new WaitForSeconds(0.5F); //waits 0.5 seconds
        Start();
    }

}
