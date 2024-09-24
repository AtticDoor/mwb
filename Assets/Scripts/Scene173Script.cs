using UnityEngine;

[System.Serializable]
public partial class Scene173Script : MonoBehaviour
{
    public GameObject[] Texts;
    public GameObject[] Spikes;
    public int index;
    public virtual void Start()
    {
        index = 0;
        while (index < 10)
        {
            Spikes[index].SetActive(false);
            ((TextMesh)Texts[index].GetComponent(typeof(TextMesh))).text = Spikes[index].name;
            index++;
        }
        index = 0;
        InvokeRepeating(nameof(EnableSpike), 4, 2);
    }

    public virtual void EnableSpike()
    {
        Texts[index].GetComponent<Renderer>().material.color = Color.black;
        Spikes[index].SetActive(true);
        index++;
        if (index >= 10)
        {
            CancelInvoke();
            InvokeRepeating(nameof(SlideshowSpike), 2, 2);
            index = 0;
            lastIndex = 9;
        }
    }

    private int lastIndex;
    public virtual void SlideshowSpike()
    {
        Texts[index].GetComponent<Renderer>().material.color = Color.magenta;
        Spikes[index].SetActive(false);
        Texts[lastIndex].GetComponent<Renderer>().material.color = Color.black;
        Spikes[lastIndex].SetActive(true);
        lastIndex = index;
        index++;
        if (index == 10)
        {
            index = 0;
        }
    }

}