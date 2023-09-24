using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class MovingPlatformParentPlayer : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Player")
        {
            other.transform.parent = this.gameObject.transform;
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