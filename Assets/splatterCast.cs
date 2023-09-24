using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class splatterCast : MonoBehaviour
{
    public GameObject splat;
    public virtual void Update()
    {
        RaycastHit hit = default(RaycastHit);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = this.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                theSplat = UnityEngine.Object.Instantiate(this.splat, hit.point + (hit.normal * 2.5f), Quaternion.identity);
                UnityEngine.Object.Destroy(theSplat, 2);
            }
        }
    }

}