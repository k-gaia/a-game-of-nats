using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkScript : MonoBehaviour
{
    public bool isActive = false;

    public void startBlink()
    {

        StartCoroutine(BlinkCo());
        isActive = true;

    }

    public void stopBlink()
    {
        StopAllCoroutines();
        isActive = false;

       




    }

    public void Update()
    {
        /*
        if (!isActive)
        {

            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            Color c = sr.color;

            float saveAlpha = c.a;

            c.a = 255f;

            sr.color = c;

            c.a = saveAlpha;
            sr.color = c;

        }
        */
    }



    IEnumerator BlinkCo()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        while (true)
        {

            yield return new WaitForSeconds(1.0f);

            Color c = sr.color;

            float saveAlpha = c.a;

            c.a = 0f;

            sr.color = c;

            yield return new WaitForSeconds(0.2f);

            c.a = saveAlpha;
            sr.color = c;

        }





    }


}
