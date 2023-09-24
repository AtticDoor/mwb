using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class mapCameraScript : MonoBehaviour
{
    public int phase;
    public GameObject src;
    public GameObject dest;
    public float startTime;
    public virtual void Start()
    {
        this.startTime = Time.time;
        Debug.Log((MainScript.curLevel + " ") + MainScript.lastLevel);
        this.phase = 1;
        this.src = GameObject.Find(("Scene" + MainScript.lastLevel) + MainScript.curLevel);
        this.dest = GameObject.Find((MainScript.curLevel + "Scene") + MainScript.lastLevel);
        Camera.main.orthographicSize = 0.07f;
        //transform.position.x=src.transform.position.x;
        //transform.position.y=src.transform.position.y;
        Vector3 srcPos = this.src.transform.position;
        srcPos.z = Camera.main.transform.position.z;
        Vector3 destPos = this.dest.transform.position;
        destPos.z = Camera.main.transform.position.z;
        this.StartCoroutine(LerpObject.MoveObject(Camera.main.transform, srcPos, destPos, 5));
    }

    public virtual void Update()
    {
        if ((Time.time - this.startTime) < 2.5f)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + (Time.deltaTime * 2);
            if (Camera.main.orthographicSize > 6)
            {
                this.phase = 3;
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