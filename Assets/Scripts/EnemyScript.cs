using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public  class EnemyScript : MonoBehaviour
{
    public bool On;
    public GameObject[] ColoredAssets;
    public int KillType; //determines end animation for player - electrocuted, fadeway, etc
    public virtual void Start()
    {
        ExtraStart();
        int i = 0;
        //cycle through colored portions, assigning them the right color in their material
        while (i < ColoredAssets.Length)
        {
            if (gameObject.tag == "blue")
                ColoredAssets[i].GetComponent<Renderer>().material = MainScript.blue;
            else if (gameObject.tag == "yellow")
                ColoredAssets[i].GetComponent<Renderer>().material = MainScript.yellow;
            else if (gameObject.tag == "red")
                ColoredAssets[i].GetComponent<Renderer>().material = MainScript.red;
            i++;
        }
    }


    public virtual void OnTriggerEnter(Collider c)
    {
        if (On)
        {
            if (c.gameObject.CompareTag("Player"))
                PlayerScript.Kill(KillType);    
        }
    }
    public virtual void Update()
    {
        ExtraUpdate();
    }

    public virtual void ExtraUpdate()
    {
    }

    public virtual void ExtraStart()
    {
        // Debug.Break();
        //On = true;
    }

}