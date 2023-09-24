using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class HeartSwirlScript : MonoBehaviour
{
    public float speed;
    public virtual void Start()
    {
        this.speed = Random.Range(-0.1f, 0.1f);
        this.transform.localScale = new Vector3(0, 0, 1);
    }

    public virtual void Update()
    {
        this.transform.Rotate(0, 0, this.speed);
        float amt = Time.deltaTime * 3;
        this.transform.localScale = this.transform.localScale + new Vector3(amt, amt, 0);
        if (this.transform.localScale.x > 5)
        {

            {
                float _158 = this.GetComponent<Renderer>().material.color.a - (amt / 8);
                Color _159 = this.GetComponent<Renderer>().material.color;
                _159.a = _158;
                this.GetComponent<Renderer>().material.color = _159;
            }
        }
        if (this.GetComponent<Renderer>().material.color.a < 0)
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

}