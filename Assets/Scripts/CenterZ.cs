using UnityEngine;

[System.Serializable]
public partial class CenterZ : MonoBehaviour
{
    public virtual void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}