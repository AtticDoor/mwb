using UnityEngine;

[System.Serializable]
public partial class aliScript : MonoBehaviour
{
    public bool Hooked;
    public float EndYpos;
    public virtual void Start()
    {
    }

    public virtual void Update()
    {
        if (Hooked)
        {
            if (transform.position.y > EndYpos)
            {

                {
                    float _72 = transform.position.y - (Time.deltaTime * 2);
                    Vector3 _73 = transform.position;
                    _73.y = _72;
                    transform.position = _73;
                }
            }
        }
        else
        {

            {
                float _74 = transform.position.y + (Time.deltaTime * 2);
                Vector3 _75 = transform.position;
                _75.y = _74;
                transform.position = _75;
            }
        }
    }

}