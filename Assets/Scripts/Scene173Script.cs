using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Scene173Script : MonoBehaviour
{
    public GameObject[] Texts;
    public GameObject[] Spikes;
    public int index;
    public virtual void Start()
    {
        this.index = 0;
        while (this.index < 10)
        {
            this.Spikes[this.index].active = false;
            ((TextMesh) this.Texts[this.index].GetComponent(typeof(TextMesh))).text = this.Spikes[this.index].name;
            this.index++;
        }
        this.index = 0;
        this.InvokeRepeating("EnableSpike", 4, 2);
    }

    public virtual void EnableSpike()
    {
        this.Texts[this.index].GetComponent<Renderer>().material.color = Color.black;
        this.Spikes[this.index].active = true;
        this.index++;
        if (this.index >= 10)
        {
            this.CancelInvoke();
            this.InvokeRepeating("SlideshowSpike", 2, 2);
            this.index = 0;
            this.lastIndex = 9;
        }
    }

    private int lastIndex;
    public virtual void SlideshowSpike()
    {
        this.Texts[this.index].GetComponent<Renderer>().material.color = Color.magenta;
        this.Spikes[this.index].active = false;
        this.Texts[this.lastIndex].GetComponent<Renderer>().material.color = Color.black;
        this.Spikes[this.lastIndex].active = true;
        this.lastIndex = this.index;
        this.index++;
        if (this.index == 10)
        {
            this.index = 0;
        }
    }

}