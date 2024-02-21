using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeInScript : MonoBehaviour
{

    private SpriteRenderer render;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        // Get The Sprite Renderer
        render = this.gameObject.GetComponent<SpriteRenderer>();

        // -> Set the Alpha to 0
        Color spriteColor = render.material.color;
        spriteColor.a = 0f;

        // Assign to Sprite Renderer 
        render.material.color = spriteColor;
        timer = 0;
    }


    IEnumerator fadeIn()
    {
        for (float f = 0.05f; f <= 1; f+= 0.08f)
        {
            Color color = this.gameObject.GetComponent<SpriteRenderer>().material.color;

            color.a = f;

            render.material.color = color;

            yield return new WaitForSeconds(0.2f);
        }
    }

    public void startFade()
    {

        StartCoroutine("fadeIn");

    }

}
