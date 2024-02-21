using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockScript : MonoBehaviour
{

    public float clockMins;
    public float clockHours;
    public float timeRate;
    public int dayPointer;
    public List<Transform> days = new List<Transform>(7);

    public Color highlightColor;

    public Text textBox;

    private Camera gameCamera;

    private ScoreScript score;
    
    public int daysSinceLastAccident;

    /*
    private Color color1 = new Color(255, 255, 255);
    private Color color2 = new Color(130, 130, 142);
    */


    private Color color1 = Color.white;
    public Color color2;

    private Transform DaysSinceLast;

    // Start is called before the first frame update
    void Start()
    {

        textBox.text = clockHours.ToString() + " : " + clockMins.ToString();

        gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        gameCamera.clearFlags = CameraClearFlags.SolidColor;

        DaysSinceLast = transform.Find("DaysSince").Find("Text");

        score = GameObject.Find("Canvas").transform.Find("Score").GetComponent<ScoreScript>();

    }

    // Update is called once per frame
    void Update()
    {

        clockMins += Time.deltaTime * timeRate;

        if (clockMins > 59)
        {

            clockMins = 0;

            clockHours++;

        }

        if (clockHours > 23)
        {

            clockHours = 0;
            dayPointer++;
            daysSinceLastAccident++;

        }

        if (dayPointer > 7)
        {

            dayPointer = 1;
            transform.Find("Days").Find("7").GetComponent<Text>().color = Color.black;
        }

        textBox.text = Mathf.Round(clockHours).ToString("00") + " : " + Mathf.Round(clockMins).ToString("00");

        float t = Mathf.PingPong(clockHours, 11.5f) / 11.5f;

        gameCamera.backgroundColor = Color.Lerp(color2, color1, t);

        transform.Find("Days").Find(dayPointer.ToString()).GetComponent<Text>().color = highlightColor;
        if (dayPointer > 1)
        {
            transform.Find("Days").Find((dayPointer - 1).ToString()).GetComponent<Text>().color = Color.black;
        }

        DaysSinceLast.GetComponent<Text>().text = "Days Since Last Accident: " + daysSinceLastAccident;


    }
}
