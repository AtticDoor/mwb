using UnityEngine;

[System.Serializable]
public partial class birdspiderhelicopter : MonoBehaviour
{
    public GameObject Player;
    public virtual void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    public bool flying;
    public virtual void Update()
    {
        if (this.flying)
        {
            this.transform.Rotate(new Vector3(0, Time.deltaTime * 4000, 0));

            {
                float _76 = this.transform.position.y + (Time.deltaTime * 5);
                Vector3 _77 = this.transform.position;
                _77.y = _76;
                this.transform.position = _77;
            }
        }
        float distance = this.Player.transform.position.x - this.transform.position.x;
        if (distance < 0)
        {
            distance = distance * -1;
        }
        if (distance < 1)
        {
            this.flying = true;
        }
    }

}