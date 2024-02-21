using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuWeatherScript : MonoBehaviour
{

    public List<GameObject> weathers = new List<GameObject>();
    public float timer;
    public float resetValue;
    //List<Vector3> spawnPoints = new List<Vector3> { };



    // Start is called before the first frame update
    void Start()
    {
        /*
        Vector3 up = transform.Find("sideUp").position;
        spawnPoints.Add(up);

        Vector3 down = transform.Find("sideDown").position;
        spawnPoints.Add(down);

        Vector3 left = transform.Find("sideLeft").position;
        spawnPoints.Add(left);

        Vector3 right = transform.Find("sideRight").position;
        spawnPoints.Add(right);
        */

        this.transform.position = new Vector3(Random.Range(222.1f, 937f), Random.Range(119.2f, 510f));

    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;

        if ( timer < 0)
        {



            Instantiate(weathers[Random.Range(0, weathers.Count)], this.transform.position, Quaternion.identity);

            this.transform.position = new Vector3(Random.Range(222.1f, 937f), Random.Range(119.2f, 510f));

            timer = resetValue;


        }



    }
}
