using System.Collections;
using UnityEngine;

[System.Serializable]
public partial class GirlHeadMainScript : MonoBehaviour
{
    public GameObject Tear;
    public Vector3[] positions;
    public int[] indexes;
    public int index;
    public virtual void Start()
    {
        InvokeRepeating("CreateTear", 2, 1);
        RandomizeIndexes();
        positions = new Vector3[8];
        positions[0] = new Vector3(0.1438493f, 0.6631676f, -0.1060616f);
        positions[1] = new Vector3(0.2244534f, 0.6696064f, -0.1060616f);
        positions[2] = new Vector3(0.07132857f, 0.624287f, -0.1060616f);
        positions[3] = new Vector3(0.2944834f, 0.7581771f, -0.1060616f);
        positions[4] = new Vector3(-0.1521176f, 0.4364915f, -0.1060616f);
        positions[5] = new Vector3(-0.2318641f, 0.3938793f, -0.1060616f);
        positions[6] = new Vector3(-0.4313891f, 0.342688f, -0.1060616f);
        positions[7] = new Vector3(-0.3340824f, 0.3427447f, -0.1060616f);
    }

    public virtual void CreateTear()
    {
        index++;
        if (index == 6)
        {
            index = 0;
        }
        if (index >= 4)
        {
            return;
        }
        StartCoroutine(CreateTear2(index));
        StartCoroutine(CreateTear2(index + 4));
    }

    public virtual IEnumerator CreateTear2(int i)
    {
        yield return new WaitForSeconds(Random.Range(0, 0.3f));
        GameObject g = UnityEngine.Object.Instantiate(Tear, transform.position, transform.rotation);
        g.transform.Rotate(0, 180, 0);
        g.transform.parent = transform;
        g.transform.localPosition = positions[indexes[i]];
    }

    public virtual void RandomizeIndexes()
    {
        indexes = new int[8];
        int i = 0;
        while (i < 8)
        {
            indexes[i] = i;
            i++;
        }
        SwapTwo();
        SwapTwo();
        SwapTwo();
    }

    public virtual void SwapTwo()
    {
        int temp = 0;
        int a = Random.Range(0, 4);
        int b = Random.Range(0, 4);
        temp = indexes[a];
        indexes[a] = indexes[b];
        indexes[b] = temp;
        a = Random.Range(4, 8);
        b = Random.Range(4, 8);
        temp = indexes[a];
        indexes[a] = indexes[b];
        indexes[b] = temp;
    }

}