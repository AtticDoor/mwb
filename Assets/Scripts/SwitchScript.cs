using UnityEngine;

[System.Serializable]
public class SwitchScript : EnemyScript
{
    public override void Start()
    {
        GetComponent<Renderer>().enabled = On;
        EnableColors(On);
    }

    public override void OnTriggerEnter(Collider c)
    {
        if (c.transform.tag == "Player")
        {
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
            this.On = !this.On;
            EnableColors(On);
        }
    }

    public virtual void EnableColors(bool t)
    {
        GameObject[] Objects = null;
        Objects = GameObject.FindGameObjectsWithTag(gameObject.tag);
        foreach (GameObject o in Objects)
        {
            //Debug.Log(o.transform.name);
            ((EnemyScript)o.GetComponent(typeof(EnemyScript))).On = t;
        }
    }
}