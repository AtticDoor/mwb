using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class MainScript : MonoBehaviour
{
    public static bool EditMode;
    public static GameObject Selected;
    public static Material yellow;
    public static Material red;
    public static Material blue;
    public static string curLevel;
    public string nonStaticCurLevel;
    public static string lastLevel;
    public static string lastExit;
    public virtual void Start()
    {
        MainScript.curLevel = this.nonStaticCurLevel;
    }

    public virtual void Update()
    {
        if (!MainScript.EditMode)
        {
            return;
        }
        float mousex = Input.mousePosition.x;
        float mousey = Input.mousePosition.y;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(mousex, mousey, 0));
        if (Input.GetMouseButtonDown(0) && (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)))
        {
            GameObject crate = UnityEngine.Object.Instantiate(this.Prefabs[MainScript.enemyIndex], ray.origin, Quaternion.identity);

            {
                int _164 = 0;
                Vector3 _165 = crate.transform.position;
                _165.z = _164;
                crate.transform.position = _165;
            }
            MainScript.Selected = crate;
        }
        if (Input.GetKeyUp("space"))
        {
            MainScript.enemyIndex++;
        }
        if (MainScript.enemyIndex >= this.Prefabs.Length)
        {
            MainScript.enemyIndex = 0;
        }
    }

    public GameObject[] Prefabs;
    public static int enemyIndex;
    public static bool verticalCam;
    public virtual void OnGUI()
    {
        int j = 0;
        GUI.depth = 0;
        GUI.Label(new Rect(0, 100, 100, 100), "Scene" + MainScript.curLevel);
        //test door output	
        if (false)
        {
            while (j < 70)
            {
                GUI.Label(new Rect(j * 50, 50, 100, 100), (j + "") + ElevatorCodes.TVCleared(j));
                j++;
            }
        }
        GUI.Label(new Rect(0, 100, 100, 100), "Scene" + MainScript.curLevel);
        if (!MainScript.EditMode)
        {
            return;
        }
        GUI.Label(new Rect(0, 0, 200, 100), "CREATE: " + this.Prefabs[MainScript.enemyIndex].transform.name);
        string SelectedName = "";
        if (MainScript.Selected != null)
        {
            SelectedName = MainScript.Selected.transform.name;
        }
        GUI.Label(new Rect(0, 20, 200, 100), "Selected: " + SelectedName);
        if (GUI.Button(new Rect(0, 40, 200, 20), "Vertical Cam: " + MainScript.verticalCam))
        {
            MainScript.verticalCam = !MainScript.verticalCam;
        }
    }

}