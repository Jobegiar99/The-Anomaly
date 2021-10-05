using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileA : MonoBehaviour
{
    public GameObject target;
    public GameObject deathEffect;
    public GameObject hitEffect;
    private Vector3 direction;
    private int HP = 5;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
        direction = new Vector3(0, 0, 0);
        StartCoroutine(UpdateDirection());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction;
    }

    private float EuclideanDistance()
    {
        float x = Mathf.Pow(transform.position.x - target.transform.position.x, 2);
        float y = Mathf.Pow(transform.position.y - target.transform.position.y, 2);
        return Mathf.Sqrt(x + y);
    }

    IEnumerator UpdateDirection()
    {
   
        while (true)
        {

            float maxSpeed = 0.03f * Random.Range(1, 1.1f); ;
            Vector2 playerPos = target.transform.position;
            direction = new Vector3((playerPos.x - transform.position.x) * Random.Range(1,1.6f)   , (playerPos.y - transform.position.y) * Random.Range(1, 1.6f) ) ;
            if (direction.x > maxSpeed) direction.x = maxSpeed;
            if (direction.x < -maxSpeed) direction.x = -maxSpeed;
            if (direction.y > maxSpeed) direction.y = maxSpeed;
            if (direction.y < -maxSpeed) direction.y = -maxSpeed;
            yield return new WaitForSecondsRealtime( Random.Range(0.5f,1.4f ));

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Projectile>() != null)
        {
            Debug.Log(HP);
            HP--;
            if (HP <= 0)
            {
                Destroy(this.gameObject);
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }
            Destroy(collision.gameObject);
        }
    }


}
