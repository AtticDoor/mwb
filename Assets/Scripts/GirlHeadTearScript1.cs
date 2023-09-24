using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class GirlHeadTearScript1 : MonoBehaviour
{
    public virtual void Start()//transform.localScale.y=Random.Range(0,.45);
    {
    }

    public virtual void Update()
    {
        if (this.transform.localScale.y < 0.45f)
        {

            {
                float _146 = this.transform.localScale.y + (Time.deltaTime / 15);
                Vector3 _147 = this.transform.localScale;
                _147.y = _146;
                this.transform.localScale = _147;
            }

            {
                int _148 = Random.Range(-3, 3);
                Vector3 _149 = this.transform.localEulerAngles;
                _149.z = _148;
                this.transform.localEulerAngles = _149;
            }
        }
        else
        {

            {
                float _150 = this.transform.position.y - (Time.deltaTime * 5);
                Vector3 _151 = this.transform.position;
                _151.y = _150;
                this.transform.position = _151;
            }
        }
    }

    public virtual void OnCollisionEnter(Collision c)
    {
        UnityEngine.Object.Destroy(this.gameObject);
    }

}