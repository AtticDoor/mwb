using UnityEngine;

[System.Serializable]
public partial class SquidTentacleStretchAndSpin : MonoBehaviour
{
    public bool Stretch;
    public float stretchMax;
    public float stretchMin;
    public float stretchAmt;
    public bool stretching;
    public float RotateAmt;
    public bool Rotating;
    public virtual void Start()
    {
        Stretch = true;
        stretching = true;
        stretchMax = 1.5f;
        stretchMin = 0.5f;
        SetStretchAmt();
        SetRotateAmt();
    }

    public virtual void Update()
    {
        if (Stretch)
        {
            //if stretching up and passed min
            //stretch down and redo rate
            if (!stretching && (transform.localScale.z < stretchMin))
            {
                stretching = true;
                SetStretchAmt();
            }
            if (stretching && (transform.localScale.z > stretchMax))
            {
                stretching = false;
                SetStretchAmt();
            }
            if (stretching)
            {
                {
                    Vector3 newPos = transform.localScale;
                    newPos.z = transform.localScale.z + (Time.deltaTime * stretchAmt); ;
                    transform.localScale = newPos;
                }
            }
            else
            {
                {
                    Vector3 newPos = transform.localScale;
                    newPos.z = transform.localScale.z - (Time.deltaTime * stretchAmt);
                    transform.localScale = newPos;
                }
            }
        }
        if (Rotating)
        {
            transform.Rotate(0, 0, Time.deltaTime * RotateAmt);
        }
    }

    public virtual void SetStretchAmt()
    {
        stretchAmt = Random.Range(0.04f, 0.13f);
    }

    public virtual void SetRotateAmt()
    {
        RotateAmt = Random.Range(-100, 100);
    }

}