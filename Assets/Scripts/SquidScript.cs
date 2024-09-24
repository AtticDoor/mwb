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
            Vector3 newPos = transform.position;
            newPos.y = transform.position.y + (Speed * Time.deltaTime);
            transform.position = newPos;
        }
        if (MovingDown)
        {
            if (transform.position.y < -18.62549f)
            {
                InitSpeed();

                {
                    Vector3 newPos = transform.position;
                    newPos.y = 90;
                    transform.position = newPos;
                }
            }
        }
        else
        {
            if (transform.position.y > 90)
            {
                InitSpeed();

                {
                    Vector3 newPos = transform.position;
                    newPos.y = -18;
                    transform.position = newPos;
                }
            }
        }
    }

    public virtual void InitSpeed()
    {
        Speed = Random.Range(0, 1f) * 3;
        if (MovingDown)
        {
            Speed *= -1;
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