using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public GameObject childCountDown;
    public GameObject bossHPUI;
    public GameObject canvas;
    public GameObject bossRoar;
    private GameObject player;
    private Vector3[] spawnOptions;
    private bool firstState;
    // Start is called before the first frame update
    void Start()
    {
        bossHPUI.SetActive(false);
        firstState = true;

        spawnOptions = new Vector3[] { new Vector3(0, 10, 0), new Vector3(10, 0, 0), new Vector3(0, -10, 0), new Vector3(-10, 0, 0) };
   
        player = GameObject.Find("player");
        GetComponent<PolygonCollider2D>().enabled = false;
        Color invisibleBoss = GetComponent<SpriteRenderer>().color;
        invisibleBoss.a = 0;
        GetComponent<SpriteRenderer>().color = invisibleBoss;
    }

    // Update is called once per frame
    void Update()
    {
        if(firstState)
        {
            if(GameObject.FindGameObjectsWithTag("BossProjectileA").Length <= 0)
            {
                firstState = false;

                transform.position = player.transform.position - this.spawnOptions[Random.Range(0, spawnOptions.Length )];
                
                Destroy(GetComponent<WanderAroundPlayer>());
                Destroy(childCountDown);

                GetComponent<BossAttackPlayer>().enabled = true;
                GetComponent<RotateAroundPlayer>().enabled = true;
                GetComponent<BossHP>().enabled = true;
                GetComponent<PolygonCollider2D>().enabled = true;

                Color invisibleBoss = GetComponent<SpriteRenderer>().color;
                invisibleBoss.a = 1;
                GetComponent<SpriteRenderer>().color = invisibleBoss;

                bossHPUI.SetActive(true);
            }
        }
        else
        {
            float xDistance = Mathf.Pow(player.transform.position.x - transform.position.x,2);
            float yDistance = Mathf.Pow(player.transform.position.y - transform.position.y,2);

            float totalDistance = Mathf.Sqrt(xDistance + yDistance );

            if ( totalDistance > 30)
            {
                transform.position = player.transform.position - this.spawnOptions[Random.Range(0, spawnOptions.Length)];
                Instantiate(bossRoar, transform.position, Quaternion.identity).GetComponent<AudioSource>().pitch = Random.Range(0.5f,1);
            }

        }
    }
}
