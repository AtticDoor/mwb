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
        //flies up as player approaches
        if (flying)
        {
            transform.Rotate(new Vector3(0, Time.deltaTime * 4000, 0));
            {
                transform.position += new Vector3(0, (Time.deltaTime * 5), 0);
            }
        }
        //if player is close, start to fly
        float distance = Player.transform.position.x - transform.position.x;
     
        if (distance < 0)
            distance *= -1;
        
        if (distance < 1)
            flying = true;
    }

}