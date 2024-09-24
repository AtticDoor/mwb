using UnityEngine;

[System.Serializable]
public partial class HeartSwirlScript : MonoBehaviour
{
    public float speed;
    public virtual void Start()
    {
        speed = Random.Range(-0.1f, 0.1f);
        transform.localScale = new Vector3(0, 0, 1);
    }

    public virtual void Update()
    {
        transform.Rotate(0, 0, speed);
        float amt = Time.deltaTime * 3;
        transform.localScale = transform.localScale + new Vector3(amt, amt, 0);
        if (transform.localScale.x > 5)
        {
            //fade away.  could also be done by lerpScript
            {
                float newAlpha = GetComponent<Renderer>().material.color.a - (amt / 8);
                Color newColor = GetComponent<Renderer>().material.color;
                newColor.a = newAlpha;
                GetComponent<Renderer>().material.color = newColor;
            }
        }
        if (GetComponent<Renderer>().material.color.a < 0)
            Destroy(gameObject);
    }

}