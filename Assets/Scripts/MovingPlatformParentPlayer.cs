using UnityEngine;

[System.Serializable]
public partial class MovingPlatformParentPlayer : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            other.transform.parent = gameObject.transform;
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.transform.name == "Player")
        {
            other.transform.parent = null;
        }
    }

}