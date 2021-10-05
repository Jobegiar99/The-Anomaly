using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileB : MonoBehaviour
{
    public GameObject HitEffect;
    public bool doubleBeam;
    public bool tripleBeam;
    public bool quadraBeam;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
 
        float maxSpeed = 0.08f;
        Vector2 playerPos = GameObject.Find("player").transform.position;
        if ( !doubleBeam  )
            direction = new Vector3(playerPos.x - transform.position.x, playerPos.y - transform.position.y, 0);

        else
            direction = new Vector3(transform.position.x - playerPos.x, transform.position.y - playerPos.y , 0);
        

        if (direction.x > maxSpeed) direction.x = maxSpeed;
        if (direction.x < -maxSpeed) direction.x = -maxSpeed;
        if (direction.y > maxSpeed) direction.y = maxSpeed;
        if (direction.y < -maxSpeed) direction.y = -maxSpeed;
        StartCoroutine(DestroyProjectile());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(1, -1, 1));
        transform.position += direction;
    }

    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Player")
        {

            collision.GetComponent<Health>().TakeDamage(1);
            Instantiate(HitEffect, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
    }
}
