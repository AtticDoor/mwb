using UnityEngine;

[System.Serializable]
public partial class SquidScript : MonoBehaviour
{
    public float Speed;
    public bool MovingDown;
    public virtual void Start()
    {
        InitSpeed();
    }

    public virtual void Update()
    {

        {
            float _174 = transform.position.y + (Speed * Time.deltaTime);
            Vector3 _175 = transform.position;
            _175.y = _174;
            transform.position = _175;
        }
        if (MovingDown)
        {
            if (transform.position.y < -18.62549f)
            {
                InitSpeed();

                {
                    int _176 = 90;
                    Vector3 _177 = transform.position;
                    _177.y = _176;
                    transform.position = _177;
                }
            }
        }
        else
        {
            if (transform.position.y > 90)
            {
                InitSpeed();

                {
                    int _178 = -18;
                    Vector3 _179 = transform.position;
                    _179.y = _178;
                    transform.position = _179;
                }
            }
        }
    }

    public virtual void InitSpeed()
    {
        Speed = Random.Range(0, 1f) * 3;
        if (MovingDown)
        {
            Speed = Speed * -1;
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        other.transform.parent = gameObject.transform;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }

}