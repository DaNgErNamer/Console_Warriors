using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIhandler : MonoBehaviour
{
    public Button button_Start;

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
