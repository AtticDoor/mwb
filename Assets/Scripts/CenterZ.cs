using UnityEngine;

[System.Serializable]
public partial class CenterZ : MonoBehaviour
{
    public virtual void Update()//Update2();
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}