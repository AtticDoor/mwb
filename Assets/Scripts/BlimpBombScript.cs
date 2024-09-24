using UnityEngine;

[System.Serializable]
public partial class BlimpBombScript : MonoBehaviour
{

    public virtual void Update()
    {
        //bombs fall once instantiated
        transform.position += new Vector3(0, -(Time.deltaTime * 2), 0);
    }

}