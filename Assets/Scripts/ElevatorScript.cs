using System.Collections.Generic;
using UnityEngine;

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
    private bool open = false;
    public virtual void Start()
    {
        ElevatorScript.static1 = false;
        ElevatorScript.static2 = false;
        ElevatorScript.static3 = false;

        //temporary code - adds all tvs in scene to elevator list of tvs to open
        if (Codes.Length == 0)
        {

            TVScript[] tests = FindObjectsOfType(typeof(TVScript)) as TVScript[];
            foreach (var t in tests)
            {
                if (t.DoorVal >= 0)
                    Codes = new List<int>(Codes) { t.DoorVal }.ToArray();
            }

        }
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
        if (Input.GetKeyDown("1"))  //hack just for testing 
            OpenDoor();

        if (open)
            return;
        Plane1.SetActive(ElevatorScript.static1);
        Plane2.SetActive(ElevatorScript.static2);
        Plane3.SetActive(ElevatorScript.static3);
        if (!DoorOpen.active)
        {
            int i = 0;
            while (i < Codes.Length)
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
        open = true;
    }
}