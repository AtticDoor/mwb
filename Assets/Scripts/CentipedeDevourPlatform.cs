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
        if (dying) //fadeout
        {
            float alphaColor = transform.GetComponent<Renderer>().material.color.a * (1 - Time.deltaTime);
            Color newColor = transform.GetComponent<Renderer>().material.color;
            newColor.a = alphaColor;
            transform.GetComponent<Renderer>().material.color = newColor;
        }
    }

    public virtual IEnumerator DestroyPlatform()
    {
        dying = true;
        yield return new WaitForSeconds(1);
        UnityEngine.Object.Destroy(gameObject);
    }

}