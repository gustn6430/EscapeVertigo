using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainUIController : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void update() 
    { 
    }
    public void GameStart()
    {
        SceneManager.LoadScene("Tutorial");
        Debug.Log("Game Start go");
    }
    public void GameExit()
    {
        Application.Quit();

    }
    public void GameLoad()
    { 
    }
    public void GameSetting()
    { 
    
    }
    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }
    private IEnumerator CloseAfterDelay() 
    {
        anim.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        anim.ResetTrigger("close");
    }
}
