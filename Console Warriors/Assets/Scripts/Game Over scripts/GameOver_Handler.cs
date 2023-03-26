using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver_Handler : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
