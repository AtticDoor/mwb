using UnityEngine;

[System.Serializable]
public partial class beetleScript : MonoBehaviour
{
    public bool MovingUp;
    public virtual void Start()
    {
        Invoke("MoveUp", Random.Range(0.3f, 0.9f));
    }

    public virtual void Update()
    {
        {
            Vector3 newPos = transform.position;
            newPos.x = transform.position.x + (0.5f * Time.deltaTime);
            transform.position = newPos;
        }
        if (MovingUp)
        {
            {
                Vector3 newPos = transform.position;
                newPos.y = transform.position.y + (0.4f * Time.deltaTime);
                transform.position = newPos;
            }
        }
        else
        {
            {
                Vector3 newPos = transform.position;
                newPos.y = transform.position.y + (-0.4f * Time.deltaTime);
                transform.position = newPos;
            }
        }
    }

    public virtual void MoveUp()
    {
        MovingUp = !MovingUp;
        Invoke(nameof(MoveUp), Random.Range(0, 0.5f));
    }

}