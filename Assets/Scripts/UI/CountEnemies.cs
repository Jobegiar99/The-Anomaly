using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountEnemies : MonoBehaviour
{
    private UnityEngine.UI.Text levelProgress;
    // Start is called before the first frame update
    void Start()
    {
        levelProgress = GameObject.Find("Level Progress").GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        levelProgress.text = "\nEnemies Remaining:\n" + GameObject.FindGameObjectsWithTag("Enemy").Length.ToString();
    }
}
