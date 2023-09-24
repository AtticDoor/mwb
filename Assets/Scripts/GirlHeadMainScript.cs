using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class GirlHeadMainScript : MonoBehaviour
{
    public GameObject Tear;
    public Vector3[] positions;
    public int[] indexes;
    public int index;
    public virtual void Start()
    {
        this.InvokeRepeating("CreateTear", 2, 1);
        this.RandomizeIndexes();
        this.positions = new Vector3[8];
        this.positions[0] = new Vector3(0.1438493f, 0.6631676f, -0.1060616f);
        this.positions[1] = new Vector3(0.2244534f, 0.6696064f, -0.1060616f);
        this.positions[2] = new Vector3(0.07132857f, 0.624287f, -0.1060616f);
        this.positions[3] = new Vector3(0.2944834f, 0.7581771f, -0.1060616f);
        this.positions[4] = new Vector3(-0.1521176f, 0.4364915f, -0.1060616f);
        this.positions[5] = new Vector3(-0.2318641f, 0.3938793f, -0.1060616f);
        this.positions[6] = new Vector3(-0.4313891f, 0.342688f, -0.1060616f);
        this.positions[7] = new Vector3(-0.3340824f, 0.3427447f, -0.1060616f);
    }

    public virtual void CreateTear()
    {
        this.index++;
        if (this.index == 6)
        {
            this.index = 0;
        }
        if (this.index >= 4)
        {
            return;
        }
        this.StartCoroutine(this.CreateTear2(this.index));
        this.StartCoroutine(this.CreateTear2(this.index + 4));
    }

    public virtual IEnumerator CreateTear2(int i)
    {
        yield return new WaitForSeconds(Random.Range(0, 0.3f));
        GameObject g = UnityEngine.Object.Instantiate(this.Tear, this.transform.position, this.transform.rotation);
        g.transform.Rotate(0, 180, 0);
        g.transform.parent = this.transform;
        g.transform.localPosition = this.positions[this.indexes[i]];
    }

    public virtual void RandomizeIndexes()
    {
        this.indexes = new int[8];
        int i = 0;
        while (i < 8)
        {
            this.indexes[i] = i;
            i++;
        }
        this.SwapTwo();
        this.SwapTwo();
        this.SwapTwo();
    }

    public virtual void SwapTwo()
    {
        int temp = 0;
        int a = Random.Range(0, 4);
        int b = Random.Range(0, 4);
        temp = this.indexes[a];
        this.indexes[a] = this.indexes[b];
        this.indexes[b] = temp;
        a = Random.Range(4, 8);
        b = Random.Range(4, 8);
        temp = this.indexes[a];
        this.indexes[a] = this.indexes[b];
        this.indexes[b] = temp;
    }

}