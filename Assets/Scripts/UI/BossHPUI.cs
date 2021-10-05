using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPUI : MonoBehaviour
{
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<UnityEngine.UI.Text>().text = "Kill the Anomaly before it destroys the galaxy!\n" + GameObject.Find("Anomaly").GetComponent<BossHP>().HP.ToString();
    }

}
