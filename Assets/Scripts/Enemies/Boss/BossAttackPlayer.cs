using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackPlayer : MonoBehaviour
{
    public GameObject projectileB;
    private BossHP Anomaly;
    // Start is called before the first frame update
    void Start()
    {
        Anomaly = GameObject.Find("Anomaly").GetComponent<BossHP>();
        StartCoroutine(ShootProjectile());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShootProjectile()
    {

        while( true)
        {
             
            float cooldown ;
            bool doubleBeam = (Anomaly.HP <= 750);
            if( Anomaly.HP <= 10)
            {
                cooldown = 0.025f;
            }
            else if( Anomaly.HP <= 50)
            {
                cooldown = 0.05f;
            }
            else if( Anomaly.HP  <= 100)
            {
                cooldown = 0.1f;

            }else if (Anomaly.HP <= 200)
            {
                cooldown = 0.2f;
            }
            else if (Anomaly.HP <= 300)
            {
                cooldown = 0.25f;
            }
            else if (Anomaly.HP <= 500)
            {
                cooldown = 0.3f;
            }
            else if (Anomaly.HP <= 700)
            {
                cooldown = 0.4f;
            }
            else
            {
                cooldown = 0.5f;
            }
            yield return new WaitForSecondsRealtime(cooldown);
            GameObject projectile = Instantiate(projectileB, transform.position, Quaternion.identity);
            projectile.transform.parent = GameObject.Find("Projectile Holder").transform;
            projectile.GetComponent<ProjectileB>().doubleBeam = false;
            if ( doubleBeam)
            {
                GameObject secondBeam = Instantiate(projectileB, transform.position, Quaternion.identity);
                secondBeam.transform.parent = GameObject.Find("Projectile Holder").transform;
                secondBeam.GetComponent<ProjectileB>().doubleBeam = true;
            }

        }
    }
}
