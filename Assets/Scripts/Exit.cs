using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Exit : MonoBehaviour
{
    public string levelName;
    public virtual void OnTriggerEnter(Collider c)//Application.LoadLevel("Scene"+levelName);
    {
        if (c.transform.name != "Player")
        {
            return;
        }
        MainScript.lastLevel = MainScript.curLevel;
        MainScript.curLevel = "Scene" + this.levelName;
        MainScript.lastExit = "Enter";
        Application.LoadLevel("Map");
    }

}