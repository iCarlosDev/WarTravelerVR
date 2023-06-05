using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu_Pausa : MonoBehaviour
{
    public GameObject QuitGame_bttn;
    public GameObject ReturnLobby_bttn;
    public GameObject ResumeGame_bttn;

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        
    }
    
    public void ReturnLobby()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
