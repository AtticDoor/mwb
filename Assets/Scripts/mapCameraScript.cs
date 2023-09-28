using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public partial class MapCameraScript : MonoBehaviour
{
    private float startTime;
    public virtual void Start()
    {
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