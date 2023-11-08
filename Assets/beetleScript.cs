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
            float _40 = transform.position.x + (0.5f * Time.deltaTime);
            Vector3 _41 = transform.position;
            _41.x = _40;
            transform.position = _41;
        }
        if (MovingUp)
        {

            {
                float _42 = transform.position.y + (0.4f * Time.deltaTime);
                Vector3 _43 = transform.position;
                _43.y = _42;
                transform.position = _43;
            }
        }
        else
        {

            {
                float _44 = transform.position.y + (-0.4f * Time.deltaTime);
                Vector3 _45 = transform.position;
                _45.y = _44;
                transform.position = _45;
            }
        }
    }

    public virtual void MoveUp()
    {
        MovingUp = !MovingUp;
        Invoke("MoveUp", Random.Range(0, 0.5f));
    }

}