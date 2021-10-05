using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseStory : MonoBehaviour
{
    // Start is called before the first frame update
    public void Close()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
