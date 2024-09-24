
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MapCameraScript : MonoBehaviour
{
    private float startTime;

    public virtual void Awake()
    {
        if (MainScript.GameType == "B") //hack to make game type B playable
        {
            ElevatorCodes.InitCodes(); //reset all door codes
            MainScript.SceneListNumber++;

            for (int i = 0; i < MainScript.SceneList.Length; i++)
            {
                Debug.Log(MainScript.SceneList[i]);
            }

            Debug.Log("MainScript.SceneListNumber" + MainScript.SceneListNumber);
            if (MainScript.SceneListNumber> MainScript.SceneList.Length-1)
                SceneManager.LoadScene("SceneMenu");


            else SceneManager.LoadScene(MainScript.SceneList[MainScript.SceneListNumber]);
        }
    }
    public virtual void Start()
    {
        if (MainScript.GameType == "B") //hack to make game type B playable
            return;

        startTime = Time.time;
        Camera.main.orthographicSize = 0.07f;

        Vector3 srcPos = GameObject.Find("Scene" + MainScript.lastLevel + MainScript.curLevel).transform.position;
        Vector3 destPos = GameObject.Find(MainScript.curLevel + "Scene" + MainScript.lastLevel).transform.position;
        srcPos.z = Camera.main.transform.position.z;
        destPos.z = Camera.main.transform.position.z;

        //move camera - zoom in and out happens separately in Update
        StartCoroutine(LerpObject.MoveObject(Camera.main.transform, srcPos, destPos, 5));
    }

    public virtual void Update()
    {
        if (MainScript.GameType == "B") //hack to make game type B playable
            return;

        //zoom in and out - motion from src to dest is in Start()
        if ((Time.time - startTime) < 2.5f)
            Camera.main.orthographicSize = Camera.main.orthographicSize + (Time.deltaTime * 2);
        else
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - (Time.deltaTime * 2);
            if (Camera.main.orthographicSize < 0.07f)
                SceneManager.LoadScene(MainScript.curLevel);
        }
    }
}