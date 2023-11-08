using UnityEngine;

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
            hit.transform.parent = transform;
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
        GetComponent<Rigidbody>().MovePosition(new Vector3(transform.position.x, transform.position.y + (5 * Time.deltaTime), transform.position.z));
    }

}