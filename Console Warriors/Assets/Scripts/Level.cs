using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
	public enum TurnState { playerDecision = 0, playerAction, enemy, rest, after };

	public Actor player;
    public Actor enemy;
    public UIHandler UI;
    public Game game;
    public AfterLevelBonusesHandler Bonuses;
	public TurnState levelState = TurnState.playerDecision;


	public Level()
	{
	}


	public void Level_Start()
    {
        Level_Core();
    }
    private void Level_Core()
    {
		if(levelState == TurnState.playerDecision)
		{
			UI.ToggleButtons(false);
			UI.turn++;
			PlayerStage();
		}

        //UI.turn++;

        //SetLevelUI();
        //PlayerStage(); // Ход игрока
        //EnemyStage(); // Ход противника
        //RestStage(); // Ход восстановления
        //AfterLever(); // Когда все походили, делаем проверки
    }


    private void SetLevelUI()
    {
        //UI.TurnDisplay.text = "Turn - " + turn.ToString();
        //UI.StageDisplay.text = "Stage - " + stage.ToString();

        //UI.LightAttackDmg_Display.text = player.LightAttack_Damage.ToString() + " DMG";
        //UI.HeavyAttackDmg_Display.text = player.HeavyAttack_Damage.ToString() + " DMG";
        //UI.PierceAttackDmg_Display.text = player.PirceAttack_Damage.ToString() + " DMG";
        //UI.ShieldUpAmount_Display.text = player.max_Shield.ToString() + " SHLD";

        //UI.LightAttackCost_Display.text = "-" + player.actions.lightAttack_cost.ToString() + " ENG";
        //UI.HeavyAttackCost_Display.text = "-" + player.actions.heavyAttack_cost.ToString() + " ENG";
        //UI.PierceAttackCost_Display.text = "-" + player.actions.pierceAttack_cost.ToString() + " ENG";
        //UI.ShieldUpCost_Display.text = "-" + player.actions.shieldUp_cost.ToString() + " ENG";

    }

	protected IEnumerator TriggerAction(Unit u)
	{


		while (true)
		{
			Debug.Log("attacking processing");
			yield return new WaitForSeconds(2f);
			Debug.Log("stop attack");
		}
	}


	private void PlayerStage()
    {
  //      bool actionSucceded = false; // Отвечает за успешность действия.
  //      player.unit.EffectsCheck(); // Проверка эффектов + их действия
  //      if (UI.button_LightAttack_clicked) actionSucceded = player.unit.actions.lightAttack.DoAttack(player, enemy);
  //      if (UI.button_PierceAttack_clicked) actionSucceded = player.unit.actions.pierceAttack.DoAttack(player, enemy);
		//if (UI.button_HeavyAttack_clicked)
		//{
		//	//player.Serialize(player.unit, "player_save.json");
		//	//enemy.Serialize(enemy.unit, "enemy_save.json");
		//	//Debug.Log("Serialization complete");
		//	actionSucceded = player.unit.actions.heavyAttack.DoAttack(player, enemy);
		//}
  //      if (UI.button_Evade_clicked) actionSucceded = player.unit.actions.tryToEvade.Do(player);
  //      if (UI.button_ShieldUp_clicked) actionSucceded = player.unit.actions.shieldUp.Do(player);
		//if (UI.button_SkipTurn_clicked)
		//{
		//	//player.unit = player.Deserialize("player_save.json");
		//	//enemy.unit = enemy.Deserialize("enemy_save.json");
		//	actionSucceded = player.unit.actions.skipTurn.Do(player);
		//}

		levelState = TurnState.playerAction;

  //      if (actionSucceded != true)
  //      {
  //          UnsuccessfulActionHappend();
  //      }
		//else
		//{
		//	//StartCoroutine(TriggerAction(player));
		//}
    }

    private void UnsuccessfulActionHappend() // Обработчик, на случай, если действие не было выполнено
    {
        Debug.Log("Выполнено неуспешное действие");
    }

    private void EnemyStage()
    {
        enemy.unit.EffectsCheck();
        enemy.AI_Work(enemy, player);
    }
    private void RestStage()
    {
        player.unit.Rest();
        enemy.unit.Rest();
    }
    private void AfterLever()
    {
        UI.Clear_Clicks();
        player.Initialization();
        enemy.Initialization();

        if (player.unit.IsDead()) GameOver();
        if (enemy.unit.IsDead()) NextStage();
    }

    private void NextStage()
    {
        Bonuses.PrepareBonuses(UI.stage, player);
        UI.stage++;
        game.NextStage();
    }
    private void GameOver()
    {
        SceneManager.LoadScene("GameEnd", LoadSceneMode.Single);
    }

	[Serializable]
	struct TestStruct
	{
		public int value;
		public string description;
		public float LightAttack_Damage { get; set; }
	};
	TestStruct tStruct;

	private void Awake()
	{
		tStruct.value = 0;
		tStruct.description = "A";
		tStruct.LightAttack_Damage = 1.337f;
	}


	private void SaveTest()
	{
		BinaryFormatter bf = new BinaryFormatter();     //NOTE: думаю, что лучше сделать в виде XML или JSON как минимум только для отладки, но не помню как
		FileStream file = File.Create(Application.persistentDataPath + "/savetest.dat");
		bf.Serialize(file, tStruct);
		//bf.Serialize(file, player);
		file.Close();
	}

	private void LoadTest()
	{
		if(File.Exists(Application.persistentDataPath + "/savetest.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savetest.dat", FileMode.Open);
			tStruct = (TestStruct)bf.Deserialize(file);
			//player = (Unit)bf.Deserialize(file);
			file.Close();
		}
	}


	private void FixedUpdate()
	{
		if (Input.GetKeyUp("["))
		{
			Debug.Log("Key [ pressed");
			tStruct.value++;
			tStruct.description += "a";
			tStruct.LightAttack_Damage += 0.1f;
			SaveTest();

		}
		else if (Input.GetKeyUp("]"))
		{
			Debug.Log("Key ] pressed");
			LoadTest();
			Debug.Log("value: " + tStruct.value);
			Debug.Log("descr: " + tStruct.description);
		}


		if(levelState == TurnState.playerAction)
		{
			if(!player.unit.isInAnimation)
			{
				EnemyStage();
				levelState = TurnState.enemy;
			}
		}
		else if(levelState == TurnState.enemy)
		{
			//NOTE: проверяем анимацию врага как выше
			if (!enemy.unit.isInAnimation)
			{
				RestStage();
				levelState = TurnState.rest;
			}
		}
		else if(levelState == TurnState.rest)
		{
			//NOTE: проверяем анимации отдыха
			AfterLever();
			levelState = TurnState.after;
		}
		else if(levelState == TurnState.after)
		{
			UI.ToggleButtons(true);
			levelState = TurnState.playerDecision;
		}
	}
}
