using System;
using System.Collections;
using System.Collections.Generic;
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
    public void Level_Start()
    {
        Level_Core();
    }
    private void Level_Core()
    {
        UI.turn++;

        //SetLevelUI();

        PlayerStage(); // Ход игрока
        EnemyStage(); // Ход противника
        RestStage(); // Ход восстановления
        AfterLever(); // Когда все походили, делаем проверки
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
        bool actionSucceded = false; // Отвечает за успешность действия.

        if (UI.button_LightAttack_clicked) actionSucceded = player.actions.LightAttack(player, enemy);
        if (UI.button_PierceAttack_clicked) actionSucceded = player.actions.PierceAttack(player, enemy);
        if (UI.button_HeavyAttack_clicked) actionSucceded = player.actions.HeavyAttack(player, enemy);
        if (UI.button_ShieldUp_clicked) actionSucceded = player.actions.ShieldUp(player);
        if (UI.button_SkipTurn_clicked) actionSucceded = player.actions.SkipTurn(player);

        if (actionSucceded != true)
        {
            UnsuccessfulActionHappend();
        }
    }

    private void UnsuccessfulActionHappend() // Обработчик, на случай, если действие не было выполнено
    {
        Debug.Log("Выполнено неуспешное действие");
    }

    private void EnemyStage()
    {
        //enemy.actions.LightAttack(enemy, player);
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
        UI.stage++;
        game.NextStage();
    }
    private void GameOver()
    {
        SceneManager.LoadScene("GameEnd", LoadSceneMode.Single);
    }
}
