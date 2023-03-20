using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public Level(UIHandler UI)
    {
        this.UI = UI;
    }
    public Units player;
    public Units enemy;
    public int stage = 0;
    public int turn = 0;
    public UIHandler UI;
    public void Level_Start(UIHandler UI)
    {
        this.UI = UI;
        stage = 0;
        turn = 0;
        Level_Core();
    }
    private void Level_Core()
    {
        turn++;
        PlayerStage(); // Ход игрока
        EnemyStage(); // Ход противника
        RestStage(); // Ход восстановления
        AfterLever(); // Когда все походили, делаем проверки
    }



    private void PlayerStage()
    {
        bool actionSucceded = false; // Отвечает за успешность действия.

        if (UI.button_LightAttack_clicked) actionSucceded = player.actions.LightAttack(player, enemy);
        if (UI.button_PierceAttack_clicked) actionSucceded = player.actions.PierceAttack(player, enemy);
        if (UI.button_HeavyAttack_clicked) actionSucceded = player.actions.HeavyAttack(player, enemy);
        if (UI.button_ShieldUp_clicked) actionSucceded =  player.actions.ShieldUp(player);
        if (UI.button_SkipTurn_clicked) actionSucceded = player.actions.SkipTurn(player);
        
        if(actionSucceded!=true)
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
        enemy.actions.LightAttack(enemy, player);
    }
    private void RestStage()
    {
        player.Rest();
        enemy.Rest();
    }
    private void AfterLever()
    {
        if (player.IsDead()) GameOver();
    }

    private Units EnemyChoose()
    {
        Units enemy = new Barbarian();
        return enemy;
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameEnd", LoadSceneMode.Single);
    }
    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
