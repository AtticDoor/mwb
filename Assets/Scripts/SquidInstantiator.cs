using UnityEngine;

[System.Serializable]
public partial class SquidInstantiator : MonoBehaviour
{
    public GameObject Squid;
    public virtual void Start()
    {
        InvokeRepeating("NewSquid", 4, 4);
    }

    public virtual void Update()
    {
    }

    public virtual void NewSquid()
    {
        GameObject g = UnityEngine.Object.Instantiate(Squid, transform.position, transform.rotation);
        g.transform.position = new Vector3(Random.Range(4, -6), -5, 0);
    }

}