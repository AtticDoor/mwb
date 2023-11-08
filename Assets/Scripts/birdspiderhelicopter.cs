using UnityEngine;

[System.Serializable]
public partial class birdspiderhelicopter : MonoBehaviour
{
    public GameObject Player;
    public virtual void Start()
    {
        Player = GameObject.Find("Player");
    }

    public bool flying;
    public virtual void Update()
    {
        if (flying)
        {
            transform.Rotate(new Vector3(0, Time.deltaTime * 4000, 0));

            {
                float _76 = transform.position.y + (Time.deltaTime * 5);
                Vector3 _77 = transform.position;
                _77.y = _76;
                transform.position = _77;
            }
        }
        float distance = Player.transform.position.x - transform.position.x;
        if (distance < 0)
        {
            distance = distance * -1;
        }
        if (distance < 1)
        {
            flying = true;
        }
    }

}