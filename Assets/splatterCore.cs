using UnityEngine;

[System.Serializable]
public partial class splatterCore : MonoBehaviour
{
    public GameObject drip;
    public RaycastHit hit;
    public virtual void Awake()
    {
        int x = -1;
        int drops = Random.Range(40, 80);
        while (x <= drops)
        {
            x++;
            Vector3 fwd = transform.TransformDirection(Random.onUnitSphere * 5);
            if (Physics.Raycast(transform.position, fwd, out hit, 10))
            {
                GameObject splatter = Instantiate(drip, hit.point + (hit.normal * 0.1f), Quaternion.FromToRotation(Vector3.up, hit.normal));
                float scaler = Random.value;

                {

                    Vector3 newPos = splatter.transform.localScale;
                    newPos.x = splatter.transform.localScale.x * scaler;
                    splatter.transform.localScale = newPos;
                }

                {
                    Vector3 newPos = splatter.transform.localScale;
                    newPos.z = splatter.transform.localScale.z * scaler;
                    splatter.transform.localScale = newPos;
                }
                int rater = Random.Range(0, 359);
                splatter.transform.RotateAround(hit.point, hit.normal, rater);
                UnityEngine.Object.Destroy(splatter, 5);
            }
        }
    }

}