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
        if (c.transform.CompareTag("Player"))
        {
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;
            On = !On;
            EnableColors(On);
        }
    }

    public virtual void EnableColors(bool t)
    {
        GameObject[] Objects = GameObject.FindGameObjectsWithTag(gameObject.tag);
        foreach (GameObject o in Objects)
        {
            ((EnemyScript)o.GetComponent(typeof(EnemyScript))).On = t;
        }
    }
}