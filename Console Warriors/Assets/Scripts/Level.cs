using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public Level()
    {
    }
    public Units player;
    public Units enemy;
    public UIHandler UI;
    public Game game;
    public AfterLevelBonusesHandler Bonuses;
    public void Level_Start()
    {
        Level_Core();
    }
    private void Level_Core()
    {
        UI.turn++;

        //SetLevelUI();

        PlayerStage(); // ��� ������
        EnemyStage(); // ��� ����������
        RestStage(); // ��� ��������������
        AfterLever(); // ����� ��� ��������, ������ ��������
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

    private void PlayerStage()
    {
        bool actionSucceded = false; // �������� �� ���������� ��������.
        player.EffectsCheck(); // �������� �������� + �� ��������
        if (UI.button_LightAttack_clicked) actionSucceded = player.actions.lightAttack.DoAttack(player, enemy);
        if (UI.button_PierceAttack_clicked) actionSucceded = player.actions.pierceAttack.DoAttack(player, enemy);
        if (UI.button_HeavyAttack_clicked) actionSucceded = player.actions.heavyAttack.DoAttack(player, enemy);
        if (UI.button_Evade_clicked) actionSucceded = player.actions.tryToEvade.Do(player);
        if (UI.button_ShieldUp_clicked) actionSucceded = player.actions.shieldUp.Do(player);
        if (UI.button_SkipTurn_clicked) actionSucceded = player.actions.skipTurn.Do(player);

        if (actionSucceded != true)
        {
            UnsuccessfulActionHappend();
        }
    }

    private void UnsuccessfulActionHappend() // ����������, �� ������, ���� �������� �� ���� ���������
    {
        Debug.Log("��������� ���������� ��������");
    }

    private void EnemyStage()
    {
        enemy.EffectsCheck();
        enemy.AI_Work(enemy, player);
    }
    private void RestStage()
    {
        player.Rest();
        enemy.Rest();
    }
    private void AfterLever()
    {
        UI.Clear_Clicks();
        player.Initialization();
        enemy.Initialization();

        if (player.IsDead()) GameOver();
        if (enemy.IsDead()) NextStage();
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
		BinaryFormatter bf = new BinaryFormatter();     //NOTE: �����, ��� ����� ������� � ���� XML ��� JSON ��� ������� ������ ��� �������, �� �� ����� ���
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
			//player = (Units)bf.Deserialize(file);
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

		//if (Input.GetKeyUp("["))
		//{
		//	Debug.Log("Key [ pressed");
		//	//tStruct.value++;
		//	//tStruct.description += "a";
		//	SaveTest();

		//}
		//else if (Input.GetKeyUp("]"))
		//{
		//	Debug.Log("Key ] pressed");
		//	LoadTest();
		//	Debug.Log("value: " + tStruct.value);
		//	Debug.Log("descr: " + tStruct.description);

		//	//player.Initialization();
		//}
	}
}
