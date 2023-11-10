using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public partial class ElevatorDoorOpenScript : MonoBehaviour
{
    public bool PlayerWithin;
    public GameObject Tutor;
    public virtual void Update()
    {
        if (PlayerWithin || Input.GetKey("2"))
        {
            if (Input.GetKey("up") || Input.GetKey("w") || Input.GetKey("2")
                || SC_MobileControls.instance.GetJoystick("JoystickLeft").y > .1f)
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
            Tutor.SetActive(PlayerWithin);
        }
    }

    public virtual void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            PlayerWithin = false;
            Tutor.SetActive(PlayerWithin);
        }
    }

}