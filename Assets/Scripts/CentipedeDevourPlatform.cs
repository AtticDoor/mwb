using System.Collections;
using UnityEngine;

[System.Serializable]
public partial class CentipedeDevourPlatform : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider C)
    {
        if (C.transform.name == "Centi")
        {
            StartCoroutine(DestroyPlatform());
        }
    }

    private bool dying;
    public virtual void Update()
    {
        if (dying)
        {

            {
                float _138 = transform.GetComponent<Renderer>().material.color.a * (1 - Time.deltaTime);
                Color _139 = transform.GetComponent<Renderer>().material.color;
                _139.a = _138;
                transform.GetComponent<Renderer>().material.color = _139;
            }
        }
    }

    public virtual IEnumerator DestroyPlatform()
    {
        dying = true;
        yield return new WaitForSeconds(1);
        UnityEngine.Object.Destroy(gameObject);
    }

}