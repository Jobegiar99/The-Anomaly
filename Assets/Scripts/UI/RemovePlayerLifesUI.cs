using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlayerLifesUI : MonoBehaviour
{

    public GameObject firstLife;
    public GameObject secondLife;
    private Health playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if( playerHealth.currentHealth == 2 && secondLife != null)
        {
            Destroy(secondLife.gameObject);
        }
        else if (playerHealth.currentHealth == 1 && firstLife != null)
        {
            Destroy(firstLife.gameObject);
        }
        
    }
}
