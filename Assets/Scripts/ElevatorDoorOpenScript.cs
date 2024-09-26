using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public partial class ElevatorDoorOpenScript : MonoBehaviour
{
    public bool PlayerWithin;
    public GameObject Tutor; //tutorial image to display button to press for the player
    public virtual void Update()
    {
        if (PlayerWithin || Input.GetKey("2"))
        {
            if (Input.GetKey("up") || Input.GetKey("w") || Input.GetKey("2")
            || ((SC_MobileControls.instance != null) && SC_MobileControls.instance.GetJoystick("JoystickLeft").y > .1f))
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
        if (c.gameObject.CompareTag("Player"))
        {
            PlayerWithin = true;
            if(Tutor!=null)
                Tutor.SetActive(PlayerWithin);
        }
    }

    public virtual void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            PlayerWithin = false;
            if (Tutor != null)
                Tutor.SetActive(PlayerWithin);
        }
    }

}