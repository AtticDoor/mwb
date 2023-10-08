using System.Collections;
using UnityEngine;

[System.Serializable]
public class HumanBulbScript : EnemyScript
{
    public Quaternion OEMRotation;
    public override void ExtraStart()
    {
        OEMRotation = transform.rotation;
    }

    public bool Alert;
    public override void Update()
    {
        RaycastHit hit = default(RaycastHit);
        if (!Alert)
        {
            Vector3 fwd = transform.TransformDirection(Vector3.left);
            if (Physics.Raycast(transform.position, fwd, out hit))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    EnableColors(gameObject.tag, true);
                    StartCoroutine(AlertEnemies());
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                }
            }
            fwd = transform.TransformDirection(Vector3.right);
            if (Physics.Raycast(transform.position, fwd, out hit))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    EnableColors(gameObject.tag, true);
                    StartCoroutine(AlertEnemies());
                    transform.rotation = Quaternion.Euler(0, -90, 0);
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
        Alert = true;
        yield return new WaitForSeconds(3);
        transform.rotation = OEMRotation;
        Alert = false;
    }

}