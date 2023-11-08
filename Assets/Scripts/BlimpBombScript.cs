using UnityEngine;

[System.Serializable]
public partial class BlimpBombScript : MonoBehaviour
{
    public virtual void Start()
    {
    }

    public virtual void Update()
    {

        {
            float _78 = transform.position.y - (Time.deltaTime * 2);
            Vector3 _79 = transform.position;
            _79.y = _78;
            transform.position = _79;
        }
    }

}