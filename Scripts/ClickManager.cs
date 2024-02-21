using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClickManager : MonoBehaviour
{

    public GameObject lastObjectClicked;
    private string[] airports = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
    public string nameOfThingHit;
    private audioManagerScript sfx;

    // Start is called before the first frame update
    void Start()
    {

        sfx = GameObject.Find("Audio Manager").GetComponent<audioManagerScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                lastObjectClicked = hit.collider.gameObject;

                nameOfThingHit = hit.collider.gameObject.name;

                Debug.Log(nameOfThingHit);


                // Plotting Route
                if (airports.Contains(nameOfThingHit) && GameObject.Find("PlotPlane") == true && !GameObject.Find("PlotPlane").gameObject.GetComponent<PlaneScript>().route.Contains(nameOfThingHit))
                {

                    sfx.Play("AirportClick");
                    GameObject.Find("PlotPlane").gameObject.GetComponent<PlaneScript>().route.Add(hit.collider.name);

                }
                

                hit.collider.attachedRigidbody.AddForce(Vector2.up);
            }
        }

    }
}
