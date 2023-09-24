using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class RotateGear : MonoBehaviour
{
    public float RotAmt;
    public virtual void Start()
    {
        this.RotAmt = Random.Range(-90, 90);
    }

    public virtual void Update()
    {
        this.transform.Rotate(0, 0, this.RotAmt * Time.deltaTime);
    }

}