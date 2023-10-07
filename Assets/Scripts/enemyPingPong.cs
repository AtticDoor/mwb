using UnityEngine;

[System.Serializable]
public class enemyPingPong : EnemyScript
{
    public GameObject LeftBoundary;
    public GameObject RightBoundary;
    private float left;
    private float right;
    public float speed;
    public override void ExtraStart()
    {
        left = LeftBoundary.transform.position.x;
        right = RightBoundary.transform.position.x;
        transform.position = new Vector3(left, transform.position.y, transform.position.z);
        MovingRight = true;

    }

    private bool MovingRight;

    public float delay=0.0f;
    private float DelayEndTime=-1;

    public override void ExtraUpdate()
    {
        if (!On)
            return;

        
        if (MovingRight && transform.position.x > right)
        {
            MovingRight = false;
            DelayEndTime = Time.time + delay;
        }
        else if (!MovingRight && transform.position.x < left)
        {
            MovingRight = true;
            DelayEndTime = Time.time + delay;
        }
        //don't do anything if the current time is less than the delay ending
        if (Time.time > DelayEndTime)
        { 
            if (MovingRight)
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            else transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
    }

    public enemyPingPong()
    {
        speed = 1;
    }

}