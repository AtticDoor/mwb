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
                    float _64 = splatter.transform.localScale.x * scaler;
                    Vector3 _65 = splatter.transform.localScale;
                    _65.x = _64;
                    splatter.transform.localScale = _65;
                }

                {
                    float _66 = splatter.transform.localScale.z * scaler;
                    Vector3 _67 = splatter.transform.localScale;
                    _67.z = _66;
                    splatter.transform.localScale = _67;
                }
                int rater = Random.Range(0, 359);
                splatter.transform.RotateAround(hit.point, hit.normal, rater);
                UnityEngine.Object.Destroy(splatter, 5);
            }
        }
    }

}