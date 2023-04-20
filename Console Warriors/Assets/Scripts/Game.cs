using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour // Game контролирует все процессы, включая логику уровней и поведение UI
{
    public Level LevelHandler;
    public UIHandler UiHandler;
    public GameObject Menu;
    // Ememy - объект пустышка для заполнения любым противником, исключительно для единого имени и удобства.
    public GameObject Enemy;

	//Ниже список всех врагов,а именно префабов добавленных через инспектор, из них будет делаться выбор.
	// Tier 1
	public GameObject[] Tier1Enemies;

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
        UiHandler.EnemyNameDisplay.text = LevelHandler.enemy.unit.unit_name;
        UiHandler.turn = 1;
    } 

    private Actor PickEnemy()
    {
        Actor unit = Enemy.GetComponentInChildren<Actor>();
        return unit;
    }

    private void SetEnemy()
    {
		InstantiatedUnit = Instantiate(Tier1Enemies[Random.Range(0, Tier1Enemies.Length)], Enemy.transform);	//NOTE: Рандомный выбор персонажей из массива префабов
		InstantiatedUnit.transform.Rotate(0f, 80f, 0f);			//NOTE: не могу понять почему сразу не создаётся не в той точке и не с тем поворотом
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

    public void SaveGame() 
    {
        LevelHandler.player.Serialize(LevelHandler.player.unit, "player_save.json");
        Debug.Log("Игрок сериализован!");
    }

    public void LoadGame()
    {
        LevelHandler.player.unit = LevelHandler.player.Deserialize("player_save.json");
        Debug.Log("Игрок загружен!");
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Menu.SetActive(true);
    }

    public void UnpauseGame()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
    }
}
