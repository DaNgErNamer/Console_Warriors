using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Units player; 
    public Units enemy;
    public int stage = 0;
    public UIHandler UI;
    private void Start()
    {
        UI.playerUI.HealthFill = 0.5f;
    }

    void StartStage()
    {
        Debug.Log("Level Script start");
        player = new Player();
        stage = 0;
    }

    public IEnumerator CoreCoroutine()
    {
        enemy = EnemyChoose(); //Barbarian for now

        while (player.health > 0 || enemy.health > 0)
        {
            stage++;
            Debug.Log("New stage started - " + stage);
           
            PlayerStage();
            EnemyStage();
            RestStage();
            yield return new WaitForSeconds(5);
        }
    }
    private void PlayerStage()
    {
        player.LightAttack(player, enemy);
        Debug.Log("player deal damage to enemy -" + enemy.LightAttack_Damage);
        Debug.Log("enemy's health now - " + player.health);
    }
    private void EnemyStage()
    {
        enemy.LightAttack(enemy, player);
        Debug.Log("enemy deal damage to player -" + enemy.LightAttack_Damage);
        Debug.Log("player's health now - " + player.health);
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
