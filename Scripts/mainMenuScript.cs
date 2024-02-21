using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{

    public void playGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        ScoreScript.gameOver = false;
        Time.timeScale = 1f;

    }

    public void quitGame()
    {

        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();

    }


}
