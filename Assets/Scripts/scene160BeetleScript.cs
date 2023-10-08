using UnityEngine;

[System.Serializable]
public partial class scene160BeetleScript : MonoBehaviour
{
    public GameObject Beetle;
    public virtual void Start()
    {
        InvokeRepeating("StartBeetle", 2, 0.05f);
    }

    public virtual void Update()
    {
    }

    public virtual void StartBeetle()
    {
        UnityEngine.Object.Instantiate(Beetle, new Vector3(-6, Random.Range(4, -3), 0), Quaternion.EulerAngles(0, 90, 270));
    }

}