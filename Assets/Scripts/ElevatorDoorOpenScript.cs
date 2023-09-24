using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ElevatorDoorOpenScript : MonoBehaviour
{
    public bool PlayerWithin;
    public virtual void Update()
    {
        if (this.PlayerWithin)
        {
            if (Input.GetKey("up") || Input.GetKey("w"))
            {
                ElevatorScript es = (ElevatorScript) this.transform.parent.GetComponent("ElevatorScript");
                MainScript.lastLevel = MainScript.curLevel;
                MainScript.lastExit = "Elevator";
                MainScript.curLevel = "Scene" + es.levelName;
                Application.LoadLevel("Map");//"Scene"+es.levelName);
            }
        }
    }

    public virtual void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            this.PlayerWithin = true;
        }
    }

    public virtual void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            this.PlayerWithin = false;
        }
    }

}