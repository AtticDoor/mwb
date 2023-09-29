using UnityEngine;

[System.Serializable]
public partial class HeadOnWallScript : MonoBehaviour
{
    public GameObject Jaw;
    public virtual void Start()
    {
        this.Invoke("Open", 0);
    }

    public virtual void Open()
    {
        this.StartCoroutine(LerpObject.RotateObject(this.Jaw.transform, new Vector3(0, 0, 7.22f), new Vector3(0, 0, -50), 4));
        this.Invoke("Close", 5);
    }

    public virtual void Close()
    {
        this.StartCoroutine(LerpObject.RotateObject(this.Jaw.transform, new Vector3(0, 0, -50), new Vector3(0, 0, 7.22f), 4));
        this.Invoke("Open", 5);
    }

}