using UnityEngine;

[System.Serializable]
public partial class RotateGear : MonoBehaviour
{
    public float RotAmt;
    public virtual void Start()
    {
        RotAmt = Random.Range(-90, 90);
    }

    public virtual void Update()
    {
        transform.Rotate(0, 0, RotAmt * Time.deltaTime);
    }

}