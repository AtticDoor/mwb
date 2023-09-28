using UnityEngine;

[System.Serializable]
public partial class mapCameraScript : MonoBehaviour
{
    public int phase;
    public GameObject src;
    public GameObject dest;
    public float startTime;
    public virtual void Start()
    {
        startTime = Time.time;
        Debug.Log((MainScript.curLevel + " ") + MainScript.lastLevel);
        phase = 1;
        src = GameObject.Find(("Scene" + MainScript.lastLevel) + MainScript.curLevel);
        dest = GameObject.Find((MainScript.curLevel + "Scene") + MainScript.lastLevel);
        Camera.main.orthographicSize = 0.07f;
        Vector3 srcPos = src.transform.position;
        srcPos.z = Camera.main.transform.position.z;
        Vector3 destPos = dest.transform.position;
        destPos.z = Camera.main.transform.position.z;
        StartCoroutine(LerpObject.MoveObject(Camera.main.transform, srcPos, destPos, 5));
    }

    public virtual void Update()
    {
        if ((Time.time - startTime) < 2.5f)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + (Time.deltaTime * 2);
            if (Camera.main.orthographicSize > 6)
            {
                phase = 3;
            }
        }
        else
        {
            // if (phase==3)
            Camera.main.orthographicSize = Camera.main.orthographicSize - (Time.deltaTime * 2);
            if (Camera.main.orthographicSize < 0.07f)
            {
                Application.LoadLevel(MainScript.curLevel);
            }
        }
    }

}