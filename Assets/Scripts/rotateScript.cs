using UnityEngine;

[System.Serializable]
public partial class rotateScript : MonoBehaviour
{
    public virtual void Update()
    {
        transform.Rotate(0, Time.deltaTime * 5, 0);
    }

}