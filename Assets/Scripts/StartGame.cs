using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Cutscene1");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    } 
    public void Quit()
    {
        Application.Quit();
    }
}
