using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlaneScript : MonoBehaviour
{

    // List of Plane Names to Generate Random Planes
    public List<string> planeNames = new List<string> { "B-2866", "B-2862", "N822DX", "N821DX", "N556NW", "N56829", "G-DHKK", "TF-FIC", "SX-RFA", "G-DHKM", "G-DHKU" };
    
    // Airports 
    public List<string> airports = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L"};

    // Planes Route
    public List<string> route = new List<string> { };

    // The Planes Label
    private GameObject textHandle;

    // In-game Time (Time Stuff)
    private ClockScript clock;
    public float targetHour;
    public float targetMinute;

    // Plane Setup
    public Sprite plottingPlaneSprite;
    public Sprite emergencyPlaneSprite;
    public string planeName;
    public string startingAirport;
    public string goalAirport;

    // Colliders & Logic for Switching between 'plane' & 'plottingPlane'
    private CircleCollider2D plottingPlaneCollider;
    private PolygonCollider2D planeCollider;
    private bool beingHeld;
    public bool plotting;

    // Member Variable for The Count of Planes to be accessed between Start and Update (To Prevent Labels Falling Off, Unique Label Object Names)
    public int planeCount;

    // Get the Click Manager (Kinda Important)
    private ClickManager clickManager;

    // Planes Flying Speed (Make Random??)
    public float flySpeed;

    // I cant remember what this is for tbh
    public bool flying = false;

    // Count for Route Logic (Emulation of For Loop)
    public int count;

    public GameObject myTrailRenderer;

    // Sound
    private audioManagerScript sfx;
    private AudioSource alarm;

    // Score Handling
    private ScoreScript score;
    private bool assignedTimeBonus;

    // Emergency Handling
    public bool emergencyActive;
    private float emergencyTimer = 0;

    // Weather Effect
    private bool isSlowed;
    public float slowFactor;


    // Start is called before the first frame update
    void Start()
    {

        sfx = GameObject.Find("Audio Manager").GetComponent<audioManagerScript>();
        alarm = GetComponent<AudioSource>();

        assignedTimeBonus = false;

        score = GameObject.Find("Score").GetComponent<ScoreScript>();
        clock = GameObject.Find("Clock").GetComponent<ClockScript>();

        // Current Time
        float currentHour = clock.clockHours;

        
        targetHour = currentHour + Random.Range(2, 5);
        if (targetHour > 23)
        {
            int[] randomTimes = { 23, 0, 1, 2, 3, 4, 5 };
            targetHour = randomTimes[Random.Range(0, randomTimes.Length)];

        }

        targetMinute = Random.Range(20, 59);

        //Cache and Disable Plotting Plane Collider
        plottingPlaneCollider = GetComponent<CircleCollider2D>();
        plottingPlaneCollider.enabled = false;

        //Cache and Enable Plane Collider
        planeCollider = GetComponent<PolygonCollider2D>();
        planeCollider.enabled = true;

        // Get the Current Plane Count
        this.planeCount = GameObject.Find("Plane Scheduler").GetComponent<PlaneScheduler>().planeCount;

        // Cache Click Manager
        clickManager = GameObject.Find("ClickManager").GetComponent<ClickManager>();

        // Give Plane Random Designation
        planeName = planeNames[Random.Range(0, planeNames.Count)];

        // Change Object Name to PlaneName
        this.gameObject.name = planeName;

        // Sort Mouse Position For Drag+Drop
        Vector2 mouselocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Give Random Start Point for New Plane
        startingAirport = airports[Random.Range(0, airports.Count)];

        // Set Goal To be Routed To
        goalAirport = airports[Random.Range(0, airports.Count)];

        // Could Still Choose Same Airport Twice?
        while (goalAirport == startingAirport)
        {

            goalAirport = airports[Random.Range(0, airports.Count)];

        }

        // Create Text Object to Display Plane Details

        GameObject textHandle = new GameObject(planeName +  " Text Handle: " + planeCount);

        textHandle.transform.SetParent(GameObject.Find("Canvas").gameObject.transform);

        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        Text planeText = textHandle.AddComponent<Text>();

        planeText.font = ArialFont;
        planeText.text = planeName + "\n" + "\n" + "\n" + "Starting: " + startingAirport + "\n" + "Goal: " + goalAirport + "\n" + targetHour.ToString("00") + ":" + targetMinute.ToString("00");
        planeText.color = Color.black;

        textHandle.transform.localScale = new Vector3(0.04f, 0.04f, 1);
        textHandle.transform.position = transform.position;


        // Trails 
        myTrailRenderer.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {

        // Drag and drop to airport
        if (beingHeld == true && plotting == false)
        {

            Vector3 mousePos;

            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);

        }

        // If not plotting do label
        if (plotting == false)
        {

            GameObject.Find(planeName + " Text Handle: " + this.planeCount).transform.position = new Vector3(this.gameObject.transform.position.x + 0.224f, this.gameObject.transform.position.y - 0.15f, this.gameObject.transform.position.z);
        }

        // Reached starting airport, commence plotting
        if (plotting == true)
        {

            this.gameObject.name = "PlotPlane";

            // If the goal airport has been added to the planes route, travel the route
            if (route.Contains(goalAirport))
            {
                traverseRoute();
            }
           

        }

        // Less than one hour - flash
        if ((targetHour - clock.clockHours) <= 1 && this.plotting == false)
        {

            if (!this.gameObject.GetComponent<BlinkScript>().isActive)
            {

                
                this.gameObject.GetComponent<BlinkScript>().startBlink();
            }
            
            // time up, remove expired planes
            if ((clock.clockHours >= targetHour) && (targetMinute - clock.clockMins < 0))
            {
                Destroy(this.gameObject);
                Destroy(GameObject.Find(planeName + " Text Handle: " + this.planeCount));
                score.score -= 100;
            }




        }

        // Emergency Logic
        if (emergencyActive)
        {
            emergencyTimer += Time.deltaTime;

            if (beingHeld)
            {

                emergencyActive = false;
                alarm.Stop();
                Destroy(this.gameObject);
                score.safteyScore += 1;


            }

            if (emergencyTimer > 8.5f)
            {
                emergencyActive = false;
                score.score -= 150;
                score.safteyScore -= 1;
                score.accidentCount++;
                clock.daysSinceLastAccident = 0;
                sfx.Play("Crash");
                alarm.Stop();
                Destroy(this.gameObject);

                

            }

        }


        if (isSlowed)
        {

            flySpeed = 0.3f - slowFactor;

        }

        else
        {

            flySpeed = 0.4f;

        }


        //Debug.Log(clock.clockHours);
        //Debug.Log(beingHeld);

    }

    

    // Drag logic 
    private void OnMouseDown()
    {

        if (Input.GetMouseButtonDown(0))
        {

            beingHeld = true;
            sfx.Play("Pickup");
            

        }

    }

    private void OnMouseUp()
    {

        beingHeld = false;

    }

    // Switch to a plane to be plotted
    public void plotPlane()
    {
        // is plotting
        plotting = true;

        // stop blinking assuming it is nearly overdue
        this.gameObject.GetComponent<BlinkScript>().stopBlink();
        
        // disable old plane collider
        planeCollider.enabled = false;

        // enable new collider
        plottingPlaneCollider.enabled = true;


        // change sprite to plotting plane
        GetComponent<SpriteRenderer>().sprite = plottingPlaneSprite;

        // scoring based on remaining time
        if (assignedTimeBonus == false)
        {
            assignedTimeBonus = true;
            int timeBonus = Mathf.RoundToInt(Mathf.Abs(((targetHour - clock.clockHours) * 100)));
            Debug.Log(timeBonus);
            score.score += timeBonus;
                

        }

        // go to middle of airport object when its been plotted
        transform.position = GameObject.Find(startingAirport).transform.position;

        // notify user plane is to be plotted STILL TO DO 

        this.gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);

    }

    // Collision Handler 
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == startingAirport)
        {
            plotPlane();
            Destroy(GameObject.Find(planeName + " Text Handle: " + this.planeCount));
        }

        if (collision.gameObject.name == "Flying Plane" && plotting)
        {
            score.score -= 500;
            score.safteyScore -= 2;
            score.accidentCount++;
            clock.daysSinceLastAccident = 0;
            sfx.Play("Crash");
            Destroy(this.gameObject);

        }

        if (collision.tag == "Weather" && plotting == true)
        {

            isSlowed = true;


        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Weather" && plotting == true)
        {

            isSlowed = false;


        }


    }

    // Start flying
    public void traverseRoute()
    {

        // Set trail effect active
        myTrailRenderer.SetActive(true);

        // isFlying
        flying = true;

        InvokeRepeating("emergencyGenerator", 2, 2);

        // Name change for GameObject.Find purposes
        this.gameObject.name = "Flying Plane";

        // Next airport in route
        GameObject nextPort;

        // End of route reached - SCORE LOGIC GOES HERE
        if (count >= route.Count)
        {
            score.score += 100;

            if (emergencyActive)
            {

                alarm.Stop();

            }

            Destroy(this.gameObject);

        }

        // Flying route logic
        else
        {

            // Next airport 
            nextPort = GameObject.Find(route[count]);

            // Speed of MoveTo function
            float step = flySpeed * Time.deltaTime;

            // Distance between two airports
            float distanceToNext = Vector2.Distance(transform.position, nextPort.transform.position);

            // Not there yet, fly
            if (distanceToNext > 0.01f)
            {

                this.gameObject.transform.position = Vector2.MoveTowards(this.transform.position, nextPort.transform.position, step);

            }

            // We're there increment the count to find the next airport, update distance variable to represent new distance
            if (distanceToNext < 0.01f)
            {
                count++;
                distanceToNext = Vector2.Distance(transform.position, nextPort.transform.position);

            }

            // Ensure distance is correct REMEMBER TO REMOVE ME PLS
            //Debug.Log((distanceToNext, count));
        }
    }

    void emergencyGenerator()
    {

        if (!emergencyActive)
        {
            List<int> targets = new List<int> { 1 };

            int chosenNumber = Random.Range(0, 5);

            if (targets.Contains(chosenNumber))

            {
                emergencyActive = true;
                isSlowed = true;
                GetComponent<SpriteRenderer>().sprite = emergencyPlaneSprite;
                Debug.Log("Emergency");
                alarm.Play();


            }

            CancelInvoke();
        }
    }

}
