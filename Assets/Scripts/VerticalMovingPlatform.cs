using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class VerticalMovingPlatform : MonoBehaviour
{
    public virtual void Start()
    {
    }

    public virtual void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            hit.transform.parent = this.transform;
        }
    }

    public virtual void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "Player")
        {
            hit.transform.parent = null;
        }
    }

    public virtual void Update()
    {
        this.GetComponent<Rigidbody>().MovePosition(new Vector3(this.transform.position.x, this.transform.position.y + (5 * Time.deltaTime), this.transform.position.z));
    }

}