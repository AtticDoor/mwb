using UnityEngine;

[System.Serializable]
public partial class HookScript : MonoBehaviour
{
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void OnTriggerEnter(Collider c)
    {
        if (c.transform.name == "ali")
        {
            GameObject g = c.gameObject;
            aliScript a = (aliScript)g.GetComponent("aliScript");
            a.Hooked = true;
            transform.parent.parent = g.transform;
        }
    }

}