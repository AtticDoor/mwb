using UnityEngine;

[System.Serializable]
public partial class rotateScript : MonoBehaviour
{
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
        this.transform.Rotate(0, Time.deltaTime * 5, 0);
    }

}