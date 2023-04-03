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
		InstantiatedUnit = Instantiate(Tier1Enemies[Random.Range(0, Tier1Enemies.Length)], Enemy.transform);	//NOTE: ��������� ����� ���������� �� ������� ��������
		InstantiatedUnit.transform.Rotate(0f, 80f, 0f);			//NOTE: �� ���� ������ ������ ����� �� �������� �� � ��� ����� � �� � ��� ���������
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
