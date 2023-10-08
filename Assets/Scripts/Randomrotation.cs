using UnityEngine;

[System.Serializable]
public partial class Randomrotation : MonoBehaviour
{
    public Vector3 RotAmt;
    public virtual void Start()
    {
        RotAmt = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(-30, 30));
    }

    public virtual void Update()
    {
        transform.Rotate(RotAmt * Time.deltaTime);
    }

}