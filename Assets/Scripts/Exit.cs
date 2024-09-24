using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public partial class Exit : MonoBehaviour
{
    //Exit Level script

    public string levelName;
    public virtual void OnTriggerEnter(Collider c)
    {
        if (c.transform.name != "Player")
            return;
        MainScript.lastLevel = MainScript.curLevel;
        MainScript.curLevel = "Scene" + levelName;
        MainScript.lastExit = "Enter";
        SceneManager.LoadScene("Map");
    }
}