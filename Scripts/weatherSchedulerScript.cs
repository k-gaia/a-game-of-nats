using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weatherSchedulerScript : MonoBehaviour
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

        this.transform.position = new Vector3(Random.Range(-5.84f, 8.87f), Random.Range(-1.8f, 5.21f));

    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;

        if ( timer < 0)
        {



            Instantiate(weathers[Random.Range(0, weathers.Count)], this.transform.position, Quaternion.identity);

            this.transform.position = new Vector3(Random.Range(-5.84f, 8.87f), Random.Range(-1.8f, 5.21f));

            timer = resetValue;


        }



    }
}
