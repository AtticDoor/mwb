using UnityEngine;

[System.Serializable]
public partial class Randomrotation : MonoBehaviour
{
    public Vector3 RotAmt;
    public virtual void Start()
    {
        this.RotAmt = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), Random.Range(-30, 30));
    }

    public virtual void Update()
    {
        this.transform.Rotate(this.RotAmt * Time.deltaTime);
    }

}