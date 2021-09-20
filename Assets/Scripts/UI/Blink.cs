using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    private bool canContinue = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlinkMain());
    }

    IEnumerator BlinkMain()
    {
        SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
        while (true)
        {

            for(int i = 0; i < 50; i++)
            {
                Color c = sprite.color;
                c.a -= 0.01f;
                sprite.color = c;
                yield return new WaitForSecondsRealtime(0.05f);
            }
            yield return new WaitForSeconds(2);

            for (int i = 0; i < 50; i++)
            {
                Color c = sprite.color;
                c.a += 0.01f;
                sprite.color = c;
                yield return new WaitForSecondsRealtime(0.05f);
            }

            yield return new WaitForSeconds(2);
        }
    }

    IEnumerator EnableView()
    {
        SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
        while ( sprite.color.a > 0)
        {
            Color c = sprite.color;
            c.a -= 1;
            sprite.color = c;
            yield return new WaitForSeconds(1);
        }
        canContinue = true;
    }

    IEnumerator DisableView()
    {
        SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();
        while (sprite.color.a < 1)
        {
            Color c = sprite.color;
            c.a += 5;
            sprite.color = c;
            yield return new WaitForSecondsRealtime(0.5f);
        }
        canContinue = true;
    }

}
