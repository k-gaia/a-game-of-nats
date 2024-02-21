using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class weatherScript : MonoBehaviour
{

    public float accelerationTime;
    public float maxSpeed;
    private Vector2 movement;
    private float timeLeft;
    private Rigidbody2D rb;
    public bool isFaded;
    public float slowFactor;
    public List<GameObject> weathers = new List<GameObject>();
    public float lifeTime;
    public bool isFading;

    private audioManagerScript sfx;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        isFaded = true;
        isFading = false;
        lifeTime = lifeTime + Random.Range(5f, 15f);
        sfx = GameObject.Find("Audio Manager").GetComponent<audioManagerScript>();

    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        lifeTime -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            movement = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            timeLeft += accelerationTime;
        }

        /*
        if (transform.position.x > 10 || transform.position.x < -8)
        {

            Destroy(this.gameObject);


        }

        if (transform.position.y > 7 || transform.position.y < -3)
        {

            Destroy(this.gameObject);


        }
        */


        if (isFaded)
        {
            this.gameObject.GetComponent<fadeInScript>().startFade();
            isFaded = false;
        }

        if (lifeTime < 0 && !isFading)
        {

            isFading = true;

            this.gameObject.GetComponent<fadeOutScript>().startFading();



        }

        if (lifeTime < -3f)
        {

            Destroy(this.gameObject);

        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Debug.Log("Aight bet");
            transform.localScale = new Vector3(6f, 6f, 1f);
            maxSpeed = 12f;


        }

    }

    void FixedUpdate()
    {
        rb.AddForce(movement * maxSpeed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Cloud Cloud(Clone)" && collision.gameObject.name == this.gameObject.name)
        {
            Debug.Log("Collided Weather");
            GameObject biggerCloud = Instantiate(weathers[0], transform.position, Quaternion.identity);
            biggerCloud.transform.localScale += gameObject.transform.localScale * 0.5f;
            Destroy(this.gameObject);

            sfx.Play("WeatherCollision");

        }

        if (collision.gameObject.name == "Thunder Cloud(Clone)" && collision.gameObject.name == this.gameObject.name)
        {
            Debug.Log("Collided Weather");
            GameObject biggerCloud = Instantiate(weathers[2], transform.position, Quaternion.identity);
            biggerCloud.transform.localScale += gameObject.transform.localScale * 0.5f;
            Destroy(this.gameObject);

            sfx.Play("WeatherCollision");

        }

        if (collision.gameObject.name == "Snow Cloud(Clone)" && collision.gameObject.name == this.gameObject.name)
        {
            Debug.Log("Collided Weather");
            GameObject biggerCloud = Instantiate(weathers[1], transform.position, Quaternion.identity);
            biggerCloud.transform.localScale += gameObject.transform.localScale * 0.5f;
            Destroy(this.gameObject);

            sfx.Play("WeatherCollision");

        }

        if (collision.tag == "Plane")
        {


            collision.GetComponent<PlaneScript>().slowFactor = this.slowFactor;


        }

    }



}
