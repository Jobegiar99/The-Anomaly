using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    public int HP;
    public GameObject deathEffect;
    public GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        HP = 1000;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Projectile>() != null)
        {
            HP--;
            if (HP <= 0)
            {
                GameObject.Find("BossHPText").GetComponent<UnityEngine.UI.Text>().text = "VICTORY";
                GameObject.Find("GameManager").GetComponent<GameManager>().IncrementEnemiesDefeated();
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
