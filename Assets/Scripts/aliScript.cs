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
        if (this.Hooked)
        {
            if (this.transform.position.y > this.EndYpos)
            {

                {
                    float _72 = this.transform.position.y - (Time.deltaTime * 2);
                    Vector3 _73 = this.transform.position;
                    _73.y = _72;
                    this.transform.position = _73;
                }
            }
        }
        else
        {

            {
                float _74 = this.transform.position.y + (Time.deltaTime * 2);
                Vector3 _75 = this.transform.position;
                _75.y = _74;
                this.transform.position = _75;
            }
        }
    }

}