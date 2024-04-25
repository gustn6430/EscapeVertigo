using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStart : MonoBehaviour
{

    public void GameScnesCtrl()
    {
        SceneManager.LoadScene("Tutorial");
        Debug.Log("Game Start go");
    }
    public void GameExit()
    {
        Application.Quit();

    }


}
