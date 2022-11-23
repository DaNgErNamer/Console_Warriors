using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private  void Level_Core()
    {
        turn++;
        PlayerStage();      
        EnemyStage();
        //RestStage();
    }
    private void PlayerStage()
    {
        if (UI.button_LightAttack_clicked) player.LightAttack(player,enemy);
        if (UI.button_PierceAttack_clicked) player.PierceAttack(player, enemy);
        if (UI.button_HeavyAttack_clicked) player.HeavyAttack(player, enemy);
        if (UI.button_ShieldUp_clicked) player.ShieldUp(player);
        if (UI.button_SkipTurn_clicked) player.SkipTurn(player);
    }
    private void EnemyStage()
    {
        enemy.LightAttack(enemy, player);
    }
    private void RestStage()
    {
        player.Rest();
        enemy.Rest();
    }


    private Units EnemyChoose()
    {
        Units enemy = new Barbarian();
        return enemy;
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
