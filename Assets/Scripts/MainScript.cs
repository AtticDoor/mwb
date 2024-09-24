using UnityEngine;

[System.Serializable]
public partial class MainScript : MonoBehaviour
{
    public static string GameType; //hacked for Game B to play list of scenes
    public static string[] SceneList; //hacked for Game B to play list of scenes
    public static int SceneListNumber; //hacked for Game B to play list of scenes

    public static bool EditMode;
    public static GameObject Selected;
    public static Material yellow;
    public static Material red;
    public static Material blue;
    public static string curLevel;
    public string nonStaticCurLevel;
    public static string lastLevel = "Scene101";
    public static string lastExit;

    public static float timer = -100;
    public virtual void Start()
    {
        curLevel = nonStaticCurLevel;
    }

    public virtual void Update()
    {
        if (!MainScript.EditMode)
        {
            return;
        }
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(mouseX, mouseY, 0));
        if (Input.GetMouseButtonDown(0) 
        && (Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl)))
        {
            GameObject crate = UnityEngine.Object.Instantiate(Prefabs[MainScript.enemyIndex], ray.origin, Quaternion.identity);

            //set crate z position to 0
            crate.transform.position = new Vector3(crate.transform.position.x, crate.transform.position.y,0);
            MainScript.Selected = crate;
        }
        if (Input.GetKeyUp("space"))
            MainScript.enemyIndex++;
        if (MainScript.enemyIndex >= Prefabs.Length)
            MainScript.enemyIndex = 0;
    }

    public GameObject[] Prefabs;
    public static int enemyIndex;
    public static bool verticalCam;
    public virtual void OnGUI()
    {
        GUI.depth = 0;
        GUI.Label(new Rect(0, 100, 100, 100), "Scene" + MainScript.curLevel);
        GUI.Label(new Rect(0, 100, 100, 100), "Scene" + MainScript.curLevel);
        if (!MainScript.EditMode)
            return;

        GUI.Label(new Rect(0, 0, 200, 100), "CREATE: " + Prefabs[MainScript.enemyIndex].transform.name);
        string SelectedName = "";
        if (MainScript.Selected != null)
            SelectedName = MainScript.Selected.transform.name;
    
        GUI.Label(new Rect(0, 20, 200, 100), "Selected: " + SelectedName);
        if (GUI.Button(new Rect(0, 40, 200, 20), "Vertical Cam: " + MainScript.verticalCam))
            MainScript.verticalCam = !MainScript.verticalCam;
    }
}