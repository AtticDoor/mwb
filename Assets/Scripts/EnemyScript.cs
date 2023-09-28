using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public partial class EnemyScript : MonoBehaviour
{
    public bool On;
    public GameObject[] ColoredAssets;
    public virtual void Start()
    {
        ExtraStart();
        int i = 0;
        while (i < ColoredAssets.Length)
        {
            if (gameObject.tag == "blue")
            {
                ColoredAssets[i].GetComponent<Renderer>().material = MainScript.blue;
            }
            else if (gameObject.tag == "yellow")
            {
                ColoredAssets[i].GetComponent<Renderer>().material = MainScript.yellow;
            }
            else if (gameObject.tag == "red")
            {
                ColoredAssets[i].GetComponent<Renderer>().material = MainScript.red;
            }
                
            i++;
        }
    }

    public virtual void ExtraStart()
    {
        On = true;
    }

    public virtual void OnTriggerEnter(Collider c)
    {
        if (On)
        {
            if (c.gameObject.tag == "Player")
            {
                Kill(c.gameObject);
                Time.timeScale = 0;
                Time.timeScale = 1f;

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                TimerGUI.Death();
            }
        }
    }

    public virtual void Kill(GameObject g)//	g.GetComponent("Bip001 Pelvis")
    {
    }

    public virtual void Update()
    {
        ExtraUpdate();
    }

    public virtual void ExtraUpdate()
    {
    }

}