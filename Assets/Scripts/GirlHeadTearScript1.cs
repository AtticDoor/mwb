using UnityEngine;

[System.Serializable]
public partial class GirlHeadTearScript1 : MonoBehaviour
{
    //TODO add this to a pool
    public virtual void Update()
    {
        if (transform.localScale.y < 0.45f)
        {
            transform.localScale += new Vector3(0, Time.deltaTime / 15, 0);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Random.Range(-3, 3));
        }
        else
            transform.position += new Vector3(0, -(Time.deltaTime * 5), 0);
    }

    public virtual void OnCollisionEnter(Collision c)
    {
        Destroy(gameObject);
    }

}