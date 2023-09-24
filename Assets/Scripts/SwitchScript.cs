using UnityEngine;
using System.Collections;

[System.Serializable]
public class SwitchScript : EnemyScript
{
    public override void Start()
    {
        this.GetComponent<Renderer>().enabled = this.On;
        this.EnableColors(this.On);
    }

    public override void OnTriggerEnter2D(Collider2D c)
    {
        if (c.transform.tag == "Player")
        {
            this.GetComponent<Renderer>().enabled = !this.GetComponent<Renderer>().enabled;
            this.On = !this.On;
            this.EnableColors(this.On);
        }
    }

    public virtual void EnableColors(bool t)
    {
        GameObject[] Objects = null;
        Objects = GameObject.FindGameObjectsWithTag(this.gameObject.tag);
        foreach (GameObject o in Objects)
        {
             //Debug.Log(o.transform.name);
            ((EnemyScript) o.GetComponent(typeof(EnemyScript))).On = t;
        }
    }

}