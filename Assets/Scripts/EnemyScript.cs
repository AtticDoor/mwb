using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class EnemyScript : MonoBehaviour
{
    public bool On;
    public GameObject[] ColoredAssets;
    public virtual void Start()
    {
        this.ExtraStart();
        int i = 0;
        while (i < this.ColoredAssets.Length)
        {
            if (this.gameObject.tag == "blue")
            {
                this.ColoredAssets[i].GetComponent<Renderer>().material = MainScript.blue;
            }
            else
            {
                if (this.gameObject.tag == "yellow")
                {
                    this.ColoredAssets[i].GetComponent<Renderer>().material = MainScript.yellow;
                }
                else
                {
                    if (this.gameObject.tag == "red")
                    {
                        this.ColoredAssets[i].GetComponent<Renderer>().material = MainScript.red;
                    }
                }
            }
            i++;
        }
    }

    public virtual void ExtraStart()
    {
        this.On = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D c)
    {
        if (this.On)
        {
            if (c.gameObject.tag == "Player")
            {
                 //Debug.Break();
                this.Kill(c.gameObject);
                //fadeOut();	
                Time.timeScale = 0;
                //c.transform.position=GameObject.Find("PlayerStartPoint").transform.position;
                //yield WaitForSeconds(2);
                Time.timeScale = 1f;
                //c.transform.position.y=300;
                //c.renderer.enabled=true;
                //Application.LoadLevel("Scene2");
                Application.LoadLevel("Scene2WWB");//+MainScript.curLevel);
                TimerGUI.Death();
            }
        }
    }

    public virtual void Kill(GameObject g)//	g.GetComponent("Bip001 Pelvis")
    {
    }

    public virtual void Update()
    {
        this.ExtraUpdate();
    }

    public virtual void ExtraUpdate()
    {
    }

}