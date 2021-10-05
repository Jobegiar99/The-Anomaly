using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCountdown : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private UnityEngine.UI.Text text;
    private int time = 60;
  
    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
        text.text = "\nDestroy all the Anomaly's eggs before it's too late! \n" + time.ToString();
        StartCoroutine(Countdown());
    }
 
    IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time--;
            if (time == 0)
            {
                player.GetComponent<Health>().TakeDamage(1000);
                text.text = "\nThe Anomaly's offspring will destroy all within this galaxy...";
                break;
            }
            else
            {
                text.text = "\nDestroy all the Anomaly's eggs before it's too late! \n" + time.ToString();
            }
        }
    }
    
}
