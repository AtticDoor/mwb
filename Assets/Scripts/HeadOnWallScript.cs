using UnityEngine;

[System.Serializable]
public partial class HeadOnWallScript : MonoBehaviour
{
    public GameObject Jaw;
    public virtual void Start()
    {
        Invoke("Open", 0);
    }

    public virtual void Open()
    {
        StartCoroutine(LerpObject.RotateObject(Jaw.transform, new Vector3(0, 0, 7.22f), new Vector3(0, 0, -50), 4));
        Invoke("Close", 5);
    }

    public virtual void Close()
    {
        StartCoroutine(LerpObject.RotateObject(Jaw.transform, new Vector3(0, 0, -50), new Vector3(0, 0, 7.22f), 4));
        Invoke("Open", 5);
    }

}