using System.Collections;
using UnityEngine;

[System.Serializable]
public class HumanBulbScript : EnemyScript
{
    public Quaternion OEMRotation;
    public override void ExtraStart()
    {
        this.OEMRotation = this.transform.rotation;
    }

    public bool Alert;
    public override void Update()
    {
        RaycastHit hit = default(RaycastHit);
        if (!this.Alert)
        {
            Vector3 fwd = this.transform.TransformDirection(Vector3.left);
            if (Physics.Raycast(this.transform.position, fwd, out hit))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    this.EnableColors(this.gameObject.tag, true);
                    this.StartCoroutine(this.AlertEnemies());
                    this.transform.rotation = Quaternion.Euler(0, 90, 0);
                }
            }
            fwd = this.transform.TransformDirection(Vector3.right);
            if (Physics.Raycast(this.transform.position, fwd, out hit))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    this.EnableColors(this.gameObject.tag, true);
                    this.StartCoroutine(this.AlertEnemies());
                    this.transform.rotation = Quaternion.Euler(0, -90, 0);
                }
            }
        }
    }

    public virtual void EnableColors(string color, bool t)
    {
        GameObject[] Objects = null;
        Objects = GameObject.FindGameObjectsWithTag(color);
        foreach (GameObject o in Objects)
        {
            ((EnemyScript)o.GetComponent(typeof(EnemyScript))).On = t;
        }
    }

    public virtual IEnumerator AlertEnemies()
    {
        this.Alert = true;
        yield return new WaitForSeconds(3);
        this.transform.rotation = this.OEMRotation;
        this.Alert = false;
    }

}