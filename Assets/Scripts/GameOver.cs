using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void Decision(int endingIndex)
    {
        PlayerPrefs.SetInt("SelectedEnding", endingIndex);

        SceneManager.LoadScene("Ending");
    }


}
