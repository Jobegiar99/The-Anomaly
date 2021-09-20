using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject player;
    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(0, 0, 0);
        StartCoroutine(UpdateDirection());
        StartCoroutine(Teleport());
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 playerPos = player.transform.position;
        transform.position += direction;

    }

    private float EuclideanDistance()
    {
        float x = Mathf.Pow(transform.position.x - player.transform.position.x, 2);
        float y = Mathf.Pow(transform.position.y - player.transform.position.y, 2);
        return Mathf.Sqrt(x + y);
    }

    IEnumerator UpdateDirection()
    {
        while (true)
        {
            float maxSpeed = 0.06f;
            Vector2 playerPos = player.transform.position;
            direction = new Vector3(playerPos.x - transform.position.x, playerPos.y - transform.position.y, 0) * 0.01f;
            if (direction.x > maxSpeed) direction.x = maxSpeed;
            if (direction.x < -maxSpeed) direction.x = -maxSpeed;
            if (direction.y > maxSpeed) direction.y = maxSpeed;
            if (direction.y < -maxSpeed) direction.y = -maxSpeed;
            yield return new WaitForSecondsRealtime(1.5f);
        }
    }

    IEnumerator Teleport()
    {
        while(true)
        {
            yield return new WaitForSeconds(15);
            int position = Random.Range(0, 3);
            switch ( position )
            {
                case 0:
                    transform.position = new Vector3(transform.position.x, player.transform.position.y - 50, 0);
                    break;
                case 1:
                    transform.position = new Vector3(transform.position.x, player.transform.position.y + 50, 0);
                    break;
                case 2:
                    transform.position = new Vector3(player.transform.position.x + 50, transform.position.y, 0);
                    break;
                case 3:
                    transform.position = new Vector3(player.transform.position.x - 50, transform.position.y - 30, 0);
                    break;
            }
        }
    }
}
