using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text textBox;
    public int score;
    public List<string> safteyGrade = new List<string> { "D", "C", "B", "A" };
    public int safteyScore;
    public int accidentCount;
    public static bool gameOver = false;
    public GameObject gameOverUI;
    public GameObject canUI;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        safteyScore = 3;

        textBox.text = "Score: " + score.ToString("0000") + "\n" + "Saftey Grading: " + safteyGrade[safteyScore] + "\n" + "Accident Count: " + accidentCount.ToString("00");

        canUI = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {

        if (safteyScore > 3)
        {
            safteyScore = 3;
        }

        textBox.text = "Score: " + score.ToString("0000") + "\n" + "Saftey Grading: " + safteyGrade[safteyScore] + "\n" + "Accident Count: " + accidentCount.ToString("00");

        // game over logic

        if ( safteyScore == 0 || score <= -1000f || accidentCount > 3)
        {

            gameOver = true;

        }

        if (gameOver)
        {


            Time.timeScale = 0f;
            gameOverUI.SetActive(true);

            pauseMenu.GameIsPaused = true;


        }


    }
}
