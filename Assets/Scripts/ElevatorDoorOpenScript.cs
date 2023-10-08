using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public partial class ElevatorDoorOpenScript : MonoBehaviour
{
    public bool PlayerWithin;
    public virtual void Update()
    {
        if (PlayerWithin)
        {
            if (Input.GetKey("up") || Input.GetKey("w"))
            {
                ElevatorScript es = (ElevatorScript)transform.parent.GetComponent("ElevatorScript");
                MainScript.lastLevel = MainScript.curLevel;
                MainScript.lastExit = "Elevator";
                MainScript.curLevel = "Scene" + es.levelName;
                SceneManager.LoadScene("Map");
            }
        }
    }

    public virtual void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            PlayerWithin = true;
        }
    }

    public virtual void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            PlayerWithin = false;
        }
    }

}