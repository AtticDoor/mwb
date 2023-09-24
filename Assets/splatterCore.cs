using UnityEngine;
using System.Collections;

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
            Vector3 fwd = this.transform.TransformDirection(Random.onUnitSphere * 5);
            if (Physics.Raycast(this.transform.position, fwd, out this.hit, 10))
            {
                splatter = UnityEngine.Object.Instantiate(this.drip, this.hit.point + (this.hit.normal * 0.1f), Quaternion.FromToRotation(Vector3.up, this.hit.normal));
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
                splatter.transform.RotateAround(this.hit.point, this.hit.normal, rater);
                UnityEngine.Object.Destroy(splatter, 5);
            }
        }
    }

}