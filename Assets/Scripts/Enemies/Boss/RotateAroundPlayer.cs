using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPlayer : MonoBehaviour
{

    public GameObject player;
    public GameObject soundEffect;
    private BossHP Anomaly;
    private short direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = 1;
        Anomaly = GameObject.Find("Anomaly").GetComponent<BossHP>();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 15, 0);
        Instantiate(soundEffect);
        StartCoroutine(ChangeDirection());
    }

    // Update is called once per frame
    void Update()
    {
        float speed;
        bool doubleBeam = (Anomaly.HP <= 750);
        if (Anomaly.HP <= 100)
        {
            speed = 100f;

        }
        else if (Anomaly.HP <= 200)
        {
            speed = 90f;
        }
        else if (Anomaly.HP <= 300)
        {
            speed = 80f;
        }
        else if (Anomaly.HP <= 500)
        {
            speed = 70f;
        }
        else if (Anomaly.HP <= 700)
        {
            speed = 60f;
        }
        else
        {
            speed = 50;
        }
        transform.RotateAround(player.transform.position * Random.Range(1,2), new Vector3(1,1,1), speed * direction *  Time.deltaTime);
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
    }

    IEnumerator ChangeDirection()
    {
        while (true)
        {
            direction *= -1;
            yield return new WaitForSeconds(10);
        }
        
    }
}
