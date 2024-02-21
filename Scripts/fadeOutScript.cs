using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeOutScript : MonoBehaviour
{

    private SpriteRenderer render;
    public bool hasFaded;

    // Start is called before the first frame update
    void Start()
    {
        // Get The Sprite Renderer
        render = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (this.gameObject.GetComponent<SpriteRenderer>().color.a == 0)
        {

            hasFaded = true;
            StopAllCoroutines();

        }

    }

    IEnumerator fadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color color = render.material.color;

            color.a = f;

            render.material.color = color;

            yield return new WaitForSeconds(0.05f);
        }
    }

    public void startFading()
    {

        StartCoroutine("fadeOut");

    }

}
