using UnityEngine;

[System.Serializable]
public partial class SquidScript : MonoBehaviour
{
    public float Speed;
    public bool MovingDown;
    public virtual void Start()
    {
        this.InitSpeed();
    }

    public virtual void Update()
    {

        {
            float _174 = this.transform.position.y + (this.Speed * Time.deltaTime);
            Vector3 _175 = this.transform.position;
            _175.y = _174;
            this.transform.position = _175;
        }
        if (this.MovingDown)
        {
            if (this.transform.position.y < -18.62549f)
            {
                this.InitSpeed();

                {
                    int _176 = 90;
                    Vector3 _177 = this.transform.position;
                    _177.y = _176;
                    this.transform.position = _177;
                }
            }
        }
        else
        {
            if (this.transform.position.y > 90)
            {
                this.InitSpeed();

                {
                    int _178 = -18;
                    Vector3 _179 = this.transform.position;
                    _179.y = _178;
                    this.transform.position = _179;
                }
            }
        }
    }

    public virtual void InitSpeed()
    {
        this.Speed = Random.Range(0, 1f) * 3;
        if (this.MovingDown)
        {
            this.Speed = this.Speed * -1;
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        other.transform.parent = this.gameObject.transform;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }

}