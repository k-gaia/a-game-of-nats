using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScheduler : MonoBehaviour
{

    public int planeCount; 
    public GameObject plane;
    public float timer;
    public float resetTimer;
    GameObject planeClone;
    private int spawnShelf = 1;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;

        if (timer < 0f)
        {

            planeClone = Instantiate(plane, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
            planeCount++;

            transform.position = transform.position + new Vector3(0.7f, 0f, 0f);

            if (transform.position.x > 0.8f && spawnShelf == 1)
            {
                spawnShelf = 2;
                transform.position = new Vector3(-2.67f, -1.78f, -172f);
            }

            if (transform.position.x > 1.02f && spawnShelf == 2)
            {
                spawnShelf = 1;
                transform.position = new Vector3(-2.67f, -0.92f, -172f);
            }
            

            timer = resetTimer;
        }


    }
}
