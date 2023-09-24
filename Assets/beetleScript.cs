using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class beetleScript : MonoBehaviour
{
    public bool MovingUp;
    public virtual void Start()
    {
        this.Invoke("MoveUp", Random.Range(0.3f, 0.9f));
    }

    public virtual void Update()
    {

        {
            float _40 = this.transform.position.x + (0.5f * Time.deltaTime);
            Vector3 _41 = this.transform.position;
            _41.x = _40;
            this.transform.position = _41;
        }
        if (this.MovingUp)
        {

            {
                float _42 = this.transform.position.y + (0.4f * Time.deltaTime);
                Vector3 _43 = this.transform.position;
                _43.y = _42;
                this.transform.position = _43;
            }
        }
        else
        {

            {
                float _44 = this.transform.position.y + (-0.4f * Time.deltaTime);
                Vector3 _45 = this.transform.position;
                _45.y = _44;
                this.transform.position = _45;
            }
        }
    }

    public virtual void MoveUp()
    {
        this.MovingUp = !this.MovingUp;
        this.Invoke("MoveUp", Random.Range(0, 0.5f));
    }

}