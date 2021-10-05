using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// This class is meant to be used on buttons as a quick easy way to load levels (scenes)
/// </summary>
public class LevelLoadButton : MonoBehaviour
{

    public GameObject generatingLevelMessage = null;
    public GameObject parentObject;

    /// <summary>
    /// Description:
    /// Loads a level according to the name provided
    /// Input:
    /// string levelToLoadName
    /// Returns:
    /// void (no return)
    /// </summary>
    /// <param name="levelToLoadName">The name of the level to load</param>
    public void LoadLevelByName(string levelToLoadName)
    {
        Time.timeScale = 1;
        Debug.Log(generatingLevelMessage);
        if (generatingLevelMessage != null)
        {
            Instantiate(generatingLevelMessage,this.transform.position,Quaternion.identity).transform.parent = parentObject.transform;
        }
        SceneManager.LoadScene(levelToLoadName);
    }
}
