using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ElevatorScript : MonoBehaviour
{
    public int[] Codes;
    public GameObject Plane1;
    public GameObject Plane2;
    public GameObject Plane3;
    public static bool static1;
    public static bool static2;
    public static bool static3;
    public string levelName;
    public GameObject DoorOpen;
    public virtual void Start()
    {
        ElevatorScript.static1 = false;
        ElevatorScript.static2 = false;
        ElevatorScript.static3 = false;
    }

    public static void DoorSwitchOn(int i)
    {
        if (i == 1)
        {
            ElevatorScript.static1 = true;
        }
        if (i == 2)
        {
            ElevatorScript.static2 = true;
        }
        if (i == 3)
        {
            ElevatorScript.static3 = true;
        }
    }

    public virtual void Update()
    {
        Plane1.SetActive(ElevatorScript.static1);
        Plane2.SetActive(ElevatorScript.static2);
        Plane3.SetActive(ElevatorScript.static3);
        if (!DoorOpen.active)
        {
            int i = 0;
            while (i < this.Codes.Length)
            {
                 //Debug.Log(ElevatorCodes.TVCleared(Codes[i])+"   "+Codes.length+" i:"+i+"  CodesI:"+Codes[i]+" ");
                if (!ElevatorCodes.TVCleared(Codes[i]))
                {
                    return;
                }
                i++;
            }
        }
        OpenDoor();
    }

    public virtual void OpenDoor()
    {
        DoorOpen.SetActive(true);
    }

}