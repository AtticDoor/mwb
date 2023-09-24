using UnityEngine;
using System.Collections;

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
        this.Stretch = true;
        this.stretching = true;
        this.stretchMax = 1.5f;
        this.stretchMin = 0.5f;
        this.SetStretchAmt();
        this.SetRotateAmt();
    }

    public virtual void Update()
    {
        if (this.Stretch)
        {
             //if stretching up and passed min
             //stretch down and redo rate
            if (!this.stretching && (this.transform.localScale.z < this.stretchMin))
            {
                this.stretching = true;
                this.SetStretchAmt();
            }
            if (this.stretching && (this.transform.localScale.z > this.stretchMax))
            {
                this.stretching = false;
                this.SetStretchAmt();
            }
            if (this.stretching)
            {

                {
                    float _180 = this.transform.localScale.z + (Time.deltaTime * this.stretchAmt);
                    Vector3 _181 = this.transform.localScale;
                    _181.z = _180;
                    this.transform.localScale = _181;
                }
            }
            else
            {

                {
                    float _182 = this.transform.localScale.z - (Time.deltaTime * this.stretchAmt);
                    Vector3 _183 = this.transform.localScale;
                    _183.z = _182;
                    this.transform.localScale = _183;
                }
            }
        }
        if (this.Rotating)
        {
            this.transform.Rotate(0, 0, Time.deltaTime * this.RotateAmt);
        }
    }

    public virtual void SetStretchAmt()
    {
        this.stretchAmt = Random.Range(0.04f, 0.13f);
    }

    public virtual void SetRotateAmt()
    {
        this.RotateAmt = Random.Range(-100, 100);
    }

}